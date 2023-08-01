using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using SQLite;
using System.IO;
using System.Collections.ObjectModel;
using System.Net;
using Rg.Plugins.Popup.Services;
using DINEPLUSBE.FldrControlPanel;
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrPopup;

namespace DINEPLUSBE.FldrEntry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTableEdit : ContentPage
    {
        private string pristrTableDesc;
        public PageTableEdit()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            try
            {
                pristrTableDesc = lblEntTableDesc.Text;
                lblEntTableDesc.Text = $"{pristrTableDesc} >>";
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong, possible error in connection", "OK");
            }
        }



        private async void BtnRetMain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageMainMenu());
            var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
            foreach (var page in pageList)
                Navigation.RemovePage(page);
        }

        private async void BtnContinueEP_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageProductEditList());
            var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
            foreach (var page in pageList)
                Navigation.RemovePage(page);
        }


        private async void BtnTableDesc_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditTable(lblTableCode.Text, pristrTableDesc), true);

        }
    }
}