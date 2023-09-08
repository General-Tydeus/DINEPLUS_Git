using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using DINEPLUS.FldrSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSO : ContentPage
    {
        public MdlTables MdlTables1;
        public static PageSO Instance;
        public string Additional = "";
        public string strDisc;

        NetworkAccess current;

        public PageSO()
        {
            Instance = this;
            InitializeComponent();
            GetDiscount();

        }
        public void CheckConnection()
        {
            current = Connectivity.NetworkAccess;
        }
        public async void GetDiscount()
        {
            //if (current == NetworkAccess.Internet)
            //{
            //    strDisc = await new ClsGetSomething().GetDiscount();
            //}
            //else
            //{
                MdlDiscount discount = await App.ClsServeMain.ImportDiscount();
                if (discount != null)
                {
                    strDisc = discount.Discount;
                }
                //else
                //{
                //    strDisc = "No discount available.";
                //}
            //}
            // await DisplayAlert("one", strDisc, "oks");
        }
        protected async override void OnAppearing()
        {
            //CheckConnection();
            //if (current == NetworkAccess.Internet)
            //{
            //    await LoadSumary();
            //}
            //else
            //{
            //Clview.ItemsSource = await App.ClsServeMain.ImportTableList();
            //}
            await LoadSumary();
        }

        public async Task LoadSumary()
        {
            //CheckConnection();
            //if (current == NetworkAccess.Internet)
            //{
            //    await App.ClsServeInsertLocal.SaveTable();
            //}
            //else
            //{
                Clview.ItemsSource = await App.ClsServeMain.ImportTableList();
            //}
        }

        private void Clview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MdlTables1 = e.CurrentSelection[0] as MdlTables;

            //await DisplayAlert("1", MdlTables1.ToString(), "ok");

            if(MdlTables1.Status == "O")
            {
                Additional = "Additional";
                Navigation.PushAsync(new PagePrevOrder(MdlTables1));
            }
            else
            {
                Additional = "New";
                Navigation.PushAsync(new PageProductList(MdlTables1));
            }
        }
    }
}