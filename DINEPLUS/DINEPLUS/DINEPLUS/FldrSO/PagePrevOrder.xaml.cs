using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using DINEPLUSWEBAPI.FldrModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePrevOrder : ContentPage
    {
        public MdlTables mdlTables11;
        List<LocaltblMain2> PreviousOrd;
        public static PagePrevOrder Instance;
        //public string Additional = "";
        public List<ModeltblMain2> listItems;
        public PagePrevOrder(MdlTables mdlTables1)
        {
            mdlTables11 = mdlTables1;
            InitializeComponent();
            BindingContext = mdlTables1;
            Instance = this;

        }
        public void clsPage3()
        {
            Navigation.RemovePage(this);
        }
        protected async override void OnAppearing()
        {
            //await DisplayAlert("table", mdlTables11.TableDocNum.ToString(), "ok");

            await LoadLV();
        }

        public async Task LoadLV()
        {
            var varlist = await new ClsListEntry().GetTblOrders(mdlTables11.TableDocNum);

            listItems = varlist;
            LV1.ItemsSource = varlist;

            double sumOfTotals = varlist.Sum(item => item.Totals);
            lblTotals.Text = $"₱{sumOfTotals:#,0.00}";



            //PreviousOrd = await App.ClsServeMain.LocaltblMainTwo(mdlTables11.TableDocNum);
            //LV1.ItemsSource = PreviousOrd;
            //lblTotals.Text = $"₱ {PreviousOrd.Sum(x => x.Totals).ToString("N2")}";
        }

        private void btnaddOrd_Clicked(object sender, EventArgs e)
        {
            
            Navigation.PushAsync(new PageProductList(mdlTables11));
        }

        private async void BtnPay_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PagePaySO(), true);

        }

        private void btnPrint_Clicked(object sender, EventArgs e)
        {

        }
    }
}