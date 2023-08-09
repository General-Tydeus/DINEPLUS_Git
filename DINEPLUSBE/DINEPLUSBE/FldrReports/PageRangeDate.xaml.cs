using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUSBE.FldrReports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRangeDate : ContentPage
    {
        public static PageRangeDate Instance;
        private string pristrrpttoopen;
        public PageRangeDate(string strHeadRpttoOpen)
        {
            InitializeComponent();
            pristrrpttoopen = strHeadRpttoOpen;
            Instance = this;
        }

        private async void BtnOpen_Clicked(object sender, EventArgs e)
        {
            if (pristrrpttoopen== "ColSum")
            {
                if (DPFromDate.Date > DPToDate.Date)
                {
                    await DisplayAlert("Information", "Beginning date is greater than ending date", "OK");
                    DPFromDate.Focus();
                }
                else
                {

                    await Navigation.PushAsync(new PageRptCollectionSum(DPFromDate.Date.ToString("MM/dd/yyyy"), DPToDate.Date.ToString("MM/dd/yyyy")));
                }
            }
            else if (pristrrpttoopen == "SalesProducts")
            {
                if (DPFromDate.Date > DPToDate.Date)
                {
                    await DisplayAlert("Information", "Beginning date is greater than ending date", "OK");
                    DPFromDate.Focus();
                }
                else
                {

                    await Navigation.PushAsync(new PageRptSalesProducts(DPFromDate.Date.ToString("MM/dd/yyyy"), DPToDate.Date.ToString("MM/dd/yyyy")));
                }

            }
        }
    }
}