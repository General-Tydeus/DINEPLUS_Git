using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrPurchases;
using DINEPLUSBE.FldrModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using DINEPLUSBE.FldrLoginPage;
using DINEPLUSBE.FldrControlPanel;

namespace DINEPLUSBE.FldrReports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRptInvSum : ContentPage
    {
        private DateTime priDTDate;
        public PageRptInvSum(DateTime DTHeadDate)
        {
            InitializeComponent();
            priDTDate = DTHeadDate;
        }


        protected async override void OnAppearing()
        {
            try
            {
                string strAsOfDateRpt = priDTDate.ToString("MM/dd/yyyy");
                var ItemInListView = await new ClsList().GetInvSum(strAsOfDateRpt);
                LV1.ItemsSource = ItemInListView;
                double dblTotalAmount = 0;
                foreach (var varlooplist in ItemInListView)
                {
                    dblTotalAmount = dblTotalAmount+varlooplist.AlsTotalCost;
                }
                lblEntTotalAmt.Text = dblTotalAmount.ToString("N2");
            }

            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong, possible error in connection", "OK");
            }
        }

    }
}

