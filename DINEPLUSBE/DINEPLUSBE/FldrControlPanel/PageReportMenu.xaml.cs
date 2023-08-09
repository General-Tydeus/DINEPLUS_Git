
using DINEPLUSBE.FldrAdjustment;
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrEntry;
using DINEPLUSBE.FldrLoginPage;
using DINEPLUSBE.FldrPurchases;
using DINEPLUSBE.FldrReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DINEPLUSBE.FldrControlPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageReportMenu : ContentPage
    {
        public PageReportMenu()
        {
            InitializeComponent();
        }

        private async void BtnInventorySum_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageAsOfDate("InvSum"));
        }

        private async void BtnCollectSum_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageRangeDate("ColSum"));
        }
        private async void BtnSalesProducts_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageRangeDate("SalesProducts"));
        }
    }
}