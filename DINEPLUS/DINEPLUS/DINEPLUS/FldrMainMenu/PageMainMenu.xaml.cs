using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PageMainMenu()
        {
            InitializeComponent();
             On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            Errors();
            Instance = this;
        }
        public void Errors()
        {

        }

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            var i = this.Children.IndexOf(this.CurrentPage);
            DisplayAlert("", i.ToString(), "ok");

        }
    }
}