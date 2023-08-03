
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrEntry;
using DINEPLUSBE.FldrLoginPage;
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
    public partial class PageEntryMenu : ContentPage
    {
       
        public PageEntryMenu()
        {
            InitializeComponent();
        }

        private async void BtnProductAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageProductAdd());
        }

        private async void BtnProductEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageProductEditList());
        }

        private async void BtnCategoryAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCategoryAdd());
        }

        private async void BtnCategoryEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCategoryEditList());
        }

        private async void BtnTableAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageTableAdd());

        }

        private async void BtnTableEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageTableEditList());
        }

        private async void BtnCustNameAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageNameAdd());
        }

        private async void BtnCustNameEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageNameEditList());
        }
    }
}