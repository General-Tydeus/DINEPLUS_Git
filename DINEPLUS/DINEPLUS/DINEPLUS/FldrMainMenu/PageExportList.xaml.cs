using DINEPLUS.FldrExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageExportList : ContentPage
    {
        public PageExportList()
        {
            InitializeComponent();
        }

        private async void btnExpPAYO_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageExport("CS"));
        }

        private async void btnExpSO_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageExport("SO"));

        }
    }
}