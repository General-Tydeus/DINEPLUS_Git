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
using DINEPLUSBE.FldrControlPanel;
using DINEPLUSBE.FldrPurchases;

namespace DINEPLUSBE.FldrEntry
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagePDName : ContentPage
	{
        public static PagePDName Instance;

        public PagePDName()
		{
			InitializeComponent ();
            Instance = this;
            LVNameList.ItemSelected += LVNameList_ItemSelected;
            LoadName();
            DPTDate.Date = DateTime.Now;
		}

        private async void LVNameList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReference.Text))
                {
                    await DisplayAlert("Information", "Reference is empty", "OK");
                }
                else
                {
                    if (e.SelectedItem != null)
                    {
                        await Navigation.PushAsync(new PagePDProductSearchList()
                        {
                            BindingContext = e.SelectedItem as ModeltblEntryName
                        });
                    }
                }
            }
            catch (Exception)
            {
                DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }

        }

        private async void LoadName()
        {
            try
            {
                LVNameList.ItemsSource = await new ClsList().GetNameForVoucher();
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