using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMainMenu : Xamarin.Forms.TabbedPage
    {
        public static PageMainMenu Instance;
        private string strLoggedIn;
        public PageMainMenu()
        {
            InitializeComponent();
             On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            //Children.Add(new PagePAYO()); // First tab
            //Children.Add(new PageSO()); // Second tab
            Instance = this;
           // CurrentPageChanged += OnTabChanged;
            OnOpeningPage();
        }

        public void DisableAllTabs()
        {
            //Xamarin.Forms.TabbedPage.IsEnabled = false;

            // DisplayAlert("Alert!", "Disable Tab Pages", "Yes", "No");
        }

        public void EnableAllTabs()
        {
            foreach (var tab in Children)
            {
                if (tab is ContentPage contentPage)
                {
                    contentPage.IsEnabled = true;
                }
            }
        }
        private async void OnOpeningPage()
        {
            strLoggedIn = Preferences.Get("LogCheck", "1");//kon get ma kuha kon wala t ee set ya new, kon set t set ee
            if (strLoggedIn == "1") //one proceed sa login 
            {
                await Navigation.PushAsync(new PageLogin());
                return;
            }
            else if (string.IsNullOrEmpty(strLoggedIn))
            {
                await Navigation.PushAsync(new PageLogin());
                return;
            }
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Do you want to LogOut?", "Yes", "No");
                if (result == true)
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
                else
                {
                    await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                }
            });
        }
    }
}