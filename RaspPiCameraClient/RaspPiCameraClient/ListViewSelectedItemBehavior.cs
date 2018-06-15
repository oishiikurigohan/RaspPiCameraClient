using Xamarin.Forms;
using System.Windows.Input;

namespace RaspPiCameraClient
{
    public class ListViewSelectedItemBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(ListViewSelectedItemBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.BindingContextChanged += (sender, e) => {
                this.BindingContext = ((ListView)sender).BindingContext;
            };
            bindable.ItemTapped += (sender, e) => {
                this.Command.Execute(e.Item);
            };
        }
    }
}