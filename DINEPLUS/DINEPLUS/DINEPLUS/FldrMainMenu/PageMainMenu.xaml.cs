using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMainMenu : ContentPage
    {

        public static PageMainMenu Instance;
        private string strLoggedIn;
        public string strUserCode { get; set; }
        public string strUserName { get; set; }
        public string strGroupCode { get; set; }
        public string strCNCode { get; set; }

        NetworkAccess current;

        public PageMainMenu()
        {
            Instance = this;
            current = Connectivity.NetworkAccess;
            InitializeComponent();
            OnOpeningPage();
        }

        private async void OnOpeningPage()
        {
            strLoggedIn = Preferences.Get("LogCheck", "1");

            if (strLoggedIn == "1") // 1 not ok not set
            {
                await Navigation.PushAsync(new PageLogin());
                //return;
            }
            else if (strLoggedIn == "2") //2 is OK
            {
                await LoadUserDetails();
            }
            else if (strLoggedIn == "3") //3 is OK not remember password
            {
                Preferences.Clear();
                await LoadUserDetails();
            }
            else if (string.IsNullOrEmpty(strLoggedIn))
            {
                await Navigation.PushAsync(new PageLogin());
            }

        }
        public async Task LoadUserDetails()
        {
            var strSMUser = await App.ClsServeMain.GetLogInInfo();

            strCNCode = strSMUser.CNCode;
            strGroupCode = strSMUser.GroupCode;
            strUserCode = strSMUser.UserCode;
            strUserName = strSMUser.UserName;


            Preferences.Set("prefUserName", strUserName);
        }
        private async void btnPayo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePAYO());
        }

        private async void btnSO_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSO());
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Attention", "Make sure you have  \n Internet data access! \n before Logging Out!.", "OK");
                return;
            }
            var result = await this.DisplayAlert("Alert!", "Do you want to LogOut?", "Yes", "No");
            if (result == true)
            {
                await Logmeout();
            }
            else
            {
                await Xamarin.Forms.Application.Current.SavePropertiesAsync();
            }
        }
        private async Task Logmeout()
        {
            Preferences.Clear();
            Preferences.Set("LogCheck", "1"); // one == login page
            await Task.Delay(500);
            await Navigation.PushAsync(new PageLogin());
            var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
            foreach (var page in pageList)
                Navigation.RemovePage(page);
        }

        private async void btnPrinter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePrinterSetup());
        }
    }
}