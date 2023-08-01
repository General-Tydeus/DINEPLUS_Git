using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net;
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrModel;

namespace DINEPLUSBE.FldrEntry
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageNameEditList : ContentPage
	{
        public PageNameEditList()
		{
			InitializeComponent ();
            LVNameList.ItemSelected += LVNameList_ItemSelected;
		}

        private async void LVNameList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                string strControlNo = (e.SelectedItem as ModeltblEntryName)?.ControlNo.ToString();
                await Navigation.PushAsync(new PageNameEdit(strControlNo));
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }

        }

        protected async override void OnAppearing()
        {
            try
            {
            LVNameList.ItemsSource = await new ClsList().GetName();
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong. Possible error in connection", "OK");
            }

        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ModeltblEntryName> ModeltblEntryName1;
            ModeltblEntryName1 = await new ClsList().GetName();
            LVNameList.ItemsSource = ModeltblEntryName1.Where(x => x.CustName.ToLower().Contains(e.NewTextValue)).ToList();
        }

    }
}