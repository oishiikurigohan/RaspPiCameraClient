### RaspPiCameraClient

![raspberrypisecuritycamerasystem_overview](https://user-images.githubusercontent.com/28979052/41449990-7c9d13c4-709f-11e8-8d0e-d11a762fb68e.png)

### Equipment used:

　① Raspberry Pi 2  Raspbian 9 Stretch Lite ver.March 2018  
　② Logitec Wireless LAN Adapter LAN-W300N/U2  
　③ HC-SR501 Body Sensor Module  
　④ Raspberry Pi Camera Rev 1.3  
　⑤ Nexus 5  Android 6.0.1  
　⑥ ThinkPad E560  Windows 10 Home  
　⑦ iPad 2  iOS 9.3.5  
   
   
 ### Setting up an environment:
　A. [Raspberry Pi 2に人感センサとカメラモジュールをつなぎ、撮影した画像をAzure Blob Storageに保存する](http://www.kurigohan.com/article/20180423_azure_security_camera_part1.html)  
　B. [Xamarin.FormsでAzure Blob Storageの画像を表示する（SAS発行Azure Functions使用）](http://www.kurigohan.com/article/20180502_azure_blob_storage_xamarin_forms.html)  
　C. Receive Notification from Azure Notification Hubs at Xamarin.  
　　ⅰ.[Xamarin.Android](http://www.kurigohan.com/article/20180515_xamarin_android_azure_notification.html)  
　　ⅱ.[Xamarin.UWP, only Foreground](http://www.kurigohan.com/article/20180518_xamarin_uwp_azure_notification.html)  
　　ⅲ.[Xamarin.UWP, Background Task](http://www.kurigohan.com/article/20180525_uwp_background_raw_notifications.html)  
　D. Pick up storage events of Azure Blob Storage, and send notifications with Azure Notification Hubs.  
　　ⅰ.[Template Notification](http://www.kurigohan.com/article/20180603_azure_event_grid.html)  
　　ⅱ.[Windows Raw Notification](http://www.kurigohan.com/article/20180525_uwp_background_raw_notifications.html)  
   
 
 ### Source code
- Raspberry Piに接続した人感センサとカメラで撮影した画像をAzure Blob Storageに保存する[Python program](https://gist.github.com/oishiikurigohan/8f52eba5666b6e1389eae0623b58dc23).
- Azure Blob Storageの保存イベントをEvent Gridで検知し通知を送信する[Azure Function (Node.js) program](https://gist.github.com/oishiikurigohan/eb8179b9d34a0945c1189e4af403355a).
- 通知を受信し画面にBlobを表示する Xamarin.Forms C# program (this repository).

 
 
 ### (info) My Development environment:
 
Windows 10 Home  
　　Microsoft Visual Studio Community 2017 ver15.7.1  
　　Xamarin ver4.10.0.442  
　　Xamarin.Android SDK   8.3.0.19  
　　Xamarin.iOS and Xamarin.Mac SDK   11.10.1.177  

macOS High Sierra ver10.13.5  
　　Xcode ver9.4  
　　Xamarin.iOS ver11.12.0.4  
