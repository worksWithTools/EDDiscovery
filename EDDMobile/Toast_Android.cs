using Android.Widget;
using ToastMessage.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]
namespace ToastMessage.Droid
{
    public class Toast_Android : Toast
    {
        public void Show(string message)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
            });
        }
    }
}