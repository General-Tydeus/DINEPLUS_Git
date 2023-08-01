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
    public partial class PageNameEdit : ContentPage
    {
        private string pristrControlNo;
        private string pristrTogglefire = "1";//1=toggle not fire, 2=toggle fire
        private string pristrCustName;
        public PageNameEdit(string strHeadControlNo)
        {
            InitializeComponent();
            pristrControlNo = strHeadControlNo;
            SWTActive.Toggled += SWTActive_Toggled;
            SWTActive.IsEnabled = true;
        }


        private async void SWTActive_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (pristrTogglefire == "2")
                {
                    HttpClient client = new HttpClient();
                    var result = await client.GetAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WebAPI/Entry/CustNameActive?strURIControlNo={pristrControlNo}&boolURIActive={e.Value}");
                   // await Navigation.PushAsync(new PageInvEditProduct(pristrProductCode));
                   // var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                   // var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
                   // foreach (var page in pageList)
                   //     Navigation.RemovePage(page);
                   //await PopupNavigation.Instance.PopAsync();
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible connection error", "OK");
            }
        }

        protected async override void OnAppearing()
        {
            try
            {
               
                var varNames = await new ClsList().GetName();
                foreach (var VL in varNames)
                {
                    pristrCustName = VL.CustName;
                    lblEntCustName.Text = $"{VL.CustName.ToString()} >>";
                    SWTActive.IsToggled = VL.Active;
                }
                pristrTogglefire = "2";
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
            await Navigation.PushAsync(new PageNameEditList());
            var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
            foreach (var page in pageList)
                Navigation.RemovePage(page);
        }

        private async void BtnCustName_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditName(pristrControlNo, pristrCustName), true);

        }
    }
}