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
        public PageSO()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await LoadSumary();
        }

        public async Task LoadSumary()
        {
            Clview.ItemsSource = await App.ClsServeMain.ImportTableList();
        }

        private void Clview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MdlTables1 = e.CurrentSelection[0] as MdlTables;

            Navigation.PushAsync(new PageProductList(MdlTables1));
        }
    }
}