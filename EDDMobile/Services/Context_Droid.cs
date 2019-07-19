
using Android.App;
using EDDMobile.Services;
using EDMobileLibrary.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Context_Droid))]
namespace EDDMobile.Services
{
    public class Context_Droid : ContextService
    {
        public string GetExternalFilesDir(string type)
        {
#if DEBUG
            var file = Application.Context.GetExternalFilesDir(type);
            return file.AbsolutePath;
#else
            return Xamarin.Essentials.FileSystem.AppDataDirectory;
#endif
        }

    }
}
