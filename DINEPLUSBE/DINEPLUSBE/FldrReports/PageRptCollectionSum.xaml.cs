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
    public partial class PageRptCollectionSum : ContentPage
    {
        private string pristrFromDate, pristrToDate;
        public PageRptCollectionSum(string strHeadFromDate, string strHeadToDate)
        {
            InitializeComponent();
            pristrFromDate = strHeadFromDate;
            pristrToDate = strHeadToDate;
        }


        protected async override void OnAppearing()
        {
            try
            {
                var ItemInListView = await new ClsList().GetColSum(pristrFromDate, pristrToDate);
                LV1.ItemsSource = ItemInListView;
                double dblTotalAmount = 0;
                foreach (var varlooplist in ItemInListView)
                {
                    dblTotalAmount = dblTotalAmount+varlooplist.CAmount;
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

