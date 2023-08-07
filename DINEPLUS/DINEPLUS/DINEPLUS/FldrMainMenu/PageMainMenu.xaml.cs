using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMainMenu : ContentPage
    {

        public static PageMainMenu Instance;

        public PageMainMenu()
        {
            Instance = this;
            InitializeComponent();
            //On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }

        private async void btnPayo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePAYO());
        }

        private async void btnSO_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSO());
        }
    }
}