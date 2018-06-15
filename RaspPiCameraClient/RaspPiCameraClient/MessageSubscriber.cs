using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Newtonsoft.Json;
using Xamarin.Forms;
using Reactive.Bindings;

namespace RaspPiCameraClient
{
    public class MessageSubscriber
    {
        public ReactiveProperty<ImageSource> BlobImageSource { get; set; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<List<string>> BlobListSource { get; } = new ReactiveProperty<List<string>>();
        public ReactiveProperty<string> SelectedItem { get; set; } = new ReactiveProperty<string>();
        public ReactiveCommand ItemTappedCommand { get; } = new ReactiveCommand();

        public MessageSubscriber()
        {
            // ListViewのItemが選択されたら、BlobのSASTokenを取得し画面に表示する
            ItemTappedCommand.Subscribe((selectedItem) => displayBlobImage(selectedItem.ToString()));

            // Read,List権限を付与したContainerのSASを取得し、Blob一覧を画面のListViewにセット
            var containerSasToken = getSasToken(Constants.GenerateSasFunctionUri, Constants.ContainerName, null, "Read,List");
            BlobListSource.Value = getBlobListAsync(Constants.BlobServiceEndpoint + Constants.ContainerName, containerSasToken).GetAwaiter().GetResult();

            // BlobのURLを含むMessageCenterのメッセージを受け取ったら、ListViewを更新しBlobのSASTokenを取得して画面に表示する
            MessagingCenter.Subscribe<Application, string>(this, Constants.MessageID, (sender, arg) => {
                BlobListSource.Value = getBlobListAsync(Constants.BlobServiceEndpoint + Constants.ContainerName, containerSasToken).GetAwaiter().GetResult();
                SelectedItem.Value = arg;
                displayBlobImage(arg);
            });
        }

        // BlobのSASTokenを取得し画面のImageコントロールに表示する
        private void displayBlobImage(string url)
        {
            var blobSasToken = getSasToken(Constants.GenerateSasFunctionUri, Constants.ContainerName, Path.GetFileName(url), "Read");
            BlobImageSource.Value = url + blobSasToken;
        }

        // ContainerまたはBlobのSASTokenを取得する
        private string getSasToken(String sasTokenUri, String container, String blobName, String permissions)
        {
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
            webClient.Headers[HttpRequestHeader.Accept] = "application/json";
            webClient.Encoding = Encoding.UTF8;

            // 問い合わせ内容をJSONオブジェクトに設定する
            var data = new RequestJsonModel();
            data.containerName = container;
            data.blobName = blobName;
            data.permission = permissions;

            // JSONをシリアライズしPOSTリクエストでAzure Functionに送信
            var reqData = JsonConvert.SerializeObject(data);
            return webClient.UploadString(new Uri(sasTokenUri), reqData);
        }

        // Container内のBlobURL一覧を取得
        private async Task<List<string>> getBlobListAsync(string uri, string sas)
        {
            // Containerへ接続しBlob一覧を取得する
            CloudBlobContainer container = new CloudBlobContainer(new Uri(uri), new StorageCredentials(sas));
            BlobContinuationToken blobToken = null;
            List<IListBlobItem> results = new List<IListBlobItem>();
            do
            {
                // 一度の問い合わせで返却されるリストは5,000件まで
                BlobResultSegment blobList = await container.ListBlobsSegmentedAsync(blobToken).ConfigureAwait(false);
                blobToken = blobList.ContinuationToken;
                results.AddRange(blobList.Results);
                // 継続Tokenがnullになったらリスト終了
            } while (blobToken != null);

            // BlobのURL一覧を返却
            var blobUriList = new List<string>();
            foreach (IListBlobItem item in results) { blobUriList.Add(item.Uri.ToString()); }
            return blobUriList;
        }
    }
}