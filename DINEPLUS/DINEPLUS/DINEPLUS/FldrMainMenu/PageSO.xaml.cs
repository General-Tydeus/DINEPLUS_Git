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



    }
}