using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net;
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrModel;

namespace DINEPLUSBE.FldrPurchases
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagePDProductSearchList : ContentPage
	{
        public static PagePDProductSearchList Instance;
        public List<ModeltblMain2PD> ModeltblMain2PDList = new List<ModeltblMain2PD>();

        public PagePDProductSearchList()
        {
            InitializeComponent();
            Instance = this;
            lblEntTotalItem.Text = "0";
            lblEntTotalAmt.Text = "0.00";
            LoadProductList();
            LVProductList.ItemSelected += LVProductList_ItemSelected;
        }

       
        private async void LVProductList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                string strISProductCode = (e.SelectedItem as ModeltblProducts)?.StockNumber.ToString();
                string strISProductDesc = (e.SelectedItem as ModeltblProducts)?.ProductDesc.ToString();
                string strISUnitM = (e.SelectedItem as ModeltblProducts)?.UnitMeasure.ToString();
                string strISSellingPrice = (e.SelectedItem as ModeltblProducts)?.SellingPrice.ToString("N2");
                string strISUCost = (e.SelectedItem as ModeltblProducts)?.UCost.ToString("N2");

                await Navigation.PushAsync(new PagePDAddQty(strISProductCode, strISProductDesc, strISUnitM, strISSellingPrice, strISUCost));
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong, possible error in connection", "OK");
            }
        }

  
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<ModeltblProducts> ModeltblProducts1;
                ModeltblProducts1 = await new ClsList().GettblProducts("1", "Nothing");
                LVProductList.ItemsSource = ModeltblProducts1.Where(x => x.ProductDesc.ToLower().Contains(e.NewTextValue)).ToList();
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong, possible error in connection", "OK");
            }
        }

        private async void IbtnShow_Clicked(object sender, EventArgs e)
        {
            if (int.Parse(lblEntTotalItem.Text) == 0)
            {
                await DisplayAlert("Information", "No item to display", "OK");
            }
            else
            {
                await Navigation.PushAsync(new PagePDCart());
            }
        }
        
        private async void LoadProductList()
        {
            try
            {
                LVProductList.ItemsSource = await new ClsList().GettblProducts("1", "Nothing");
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong. Possible error in connection", "OK");
            }
        }

        protected override void OnAppearing()
        {
          
           lblEntTotalItem.Text = ModeltblMain2PDList.Count.ToString();
            double dblTotalAmount = 0;
            foreach (var varlooplist in ModeltblMain2PDList)
            {
                dblTotalAmount = dblTotalAmount + (varlooplist.PIn * varlooplist.UCost);
            }
            lblEntTotalAmt.Text = dblTotalAmount.ToString("N2");
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (int.Parse(lblEntTotalItem.Text) > 0)
                {
                    var result = await this.DisplayAlert("Information", "Product encoded will be erased, continue?", "Yes", "No");
                    if (result == true)
                    {
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await Application.Current.SavePropertiesAsync();
                    }
                }
                else
                {
                    await Navigation.PopAsync();
                }
                    
            });
            return true;
        }

        private void SBSearch_Focused(object sender, FocusEventArgs e)
        {
            SBSearch.Text = "";
        }
    }
}