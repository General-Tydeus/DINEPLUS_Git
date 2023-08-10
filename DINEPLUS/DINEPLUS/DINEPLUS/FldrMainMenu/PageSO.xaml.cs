using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using DINEPLUS.FldrSO;
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
    public partial class PageSO : ContentPage
    {
        public MdlTables MdlTables1;
        public static PageSO Instance;
        public string Additional = "NA";
        public PageSO()
        {
            Instance = this;
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            await LoadSumary();
        }

        public async Task LoadSumary()
        {
            //Clview.ItemsSource = await App.ClsServeMain.ImportTableList();    
            var varlist = await new ClsListEntry().GetTblList();

            Clview.ItemsSource = varlist;

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
                Navigation.PushAsync(new PageProductList(MdlTables1));
            }
        }
    }
}