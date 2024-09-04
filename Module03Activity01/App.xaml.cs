using System.Diagnostics;
using Module03Activity01.View;
using Microsoft.Maui.ApplicationModel;
using System.Threading.Tasks;
using Module03Activity01.Resources.Styles;

namespace Module03Activity01
{
    public partial class App : Application
    {
        private const string TestUrl = "https://www.gosad221ogle.com";


        public App()
        {
            InitializeComponent();

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                this.Resources.MergedDictionaries.Add(new WindowsResources());
            }

            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                this.Resources.MergedDictionaries.Add(new AndroidResources());
            }

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            //base.OnStart();
            var current = Connectivity.NetworkAccess;
            bool isWebsiteReachable = await IsWebsiteReachable(TestUrl);

            if (current == NetworkAccess.Internet && isWebsiteReachable)
            {
                MainPage = new StartPage();
                Debug.WriteLine("Application Started (Online)");
            }

            else
            {
                MainPage = new OfflinePage();
                Debug.WriteLine("Application Started (Offline)");
            }
        }

        protected override void OnSleep()
        {
            //base.OnSleep();
            Debug.WriteLine("Application Sleeping");
        }

        protected override void OnResume()
        {
            //base.OnResume();
            Debug.WriteLine("Application Resumed");
        }

        private async Task<bool> IsWebsiteReachable(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
            }

            catch
            {
                return false;
            }
        }
    }
}
