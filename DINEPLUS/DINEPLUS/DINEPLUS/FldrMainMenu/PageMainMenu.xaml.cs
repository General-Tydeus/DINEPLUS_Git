using System;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMainMenu : Xamarin.Forms.TabbedPage
    {

        public static PageMainMenu Instance;

        public PageMainMenu()
        {
            Instance = this;
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }


        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            var i = this.Children.IndexOf(this.CurrentPage);
            //DisplayAlert("", i.ToString(), "ok");
        }


    }
}