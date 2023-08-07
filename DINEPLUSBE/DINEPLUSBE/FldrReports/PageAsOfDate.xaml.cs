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
    public partial class PageAsOfDate : ContentPage
    {
        public static PageAsOfDate Instance;
        private string pristrrpttoopen;
        public PageAsOfDate(string strHeadRpttoOpen)
        {
            InitializeComponent();
            pristrrpttoopen = strHeadRpttoOpen;
            Instance = this;
        }

        private async void BtnOpen_Clicked(object sender, EventArgs e)
        {
            if (pristrrpttoopen== "InvSum")
            {
                await Navigation.PushAsync(new PageRptInvSum(DPAsOfDate.Date));
            }

        }
    }
}