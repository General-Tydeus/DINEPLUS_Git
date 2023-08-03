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

namespace DINEPLUSBE.FldrPurchases
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageProductSearchList : ContentPage
	{
        private string pristrDeliveryAreaCode;
        int intRowNum = 0;
        int IntPickup = 0;
        private string pristrTogglefire = "1";//1=toggle not fire, 2=toggle fire
        private bool boolPickup;
        public PageProductSearchList()
        {
            InitializeComponent();
            lblEntTotalItem.Text = "0";
            lblEntTotalAmt.Text = "0.00";
       
            LVProductList.ItemSelected += LVProductList_ItemSelected;

        }

       
        private async void LVProductList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {

                string strISProductCode = (e.SelectedItem as ClsViewProductList)?.ProductCode.ToString();
                string strISProductDesc = (e.SelectedItem as ClsViewProductList)?.ProductDesc.ToString();
                string strISUnitM = (e.SelectedItem as ClsViewProductList)?.UnitM.ToString();
                string strISSellingPrice = (e.SelectedItem as ClsViewProductList)?.SellingPrice.ToString("N2");
                string strISUCost = (e.SelectedItem as ClsViewProductList)?.UCost.ToString("N2");

                await Navigation.PushAsync(new PageOrder(strISProductCode, strISProductDesc, strISSellingPrice, strISUnitM, "2", strISUCost));
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong, possible error in connection", "OK");
            }
        }

  
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            { 
            var varpristrControlNumber = await App.ClsServeList.GetCurrentCustomer();
            pristrDeliveryAreaCode = varpristrControlNumber.DeliveryAreaCode.ToString();

            var varloop = await App.ClsServeWebAPI.GetProductListAllSearch(pristrDeliveryAreaCode, SBSearch.Text);
            var groupedData =
                varloop.OrderBy(p => p.WaitingTimeDesc)
                    //.GroupBy(p => p.PDFDeptDesc[0].ToString())
                    .GroupBy(p => p.WaitingTimeDesc.ToString())
                    .Select(p => new ObservableGroupCollection<string, ClsViewProductList>(p))
                    .ToList();
            BindingContext = new ObservableCollection<ObservableGroupCollection<string, ClsViewProductList>>(groupedData);
            TotalItemOrdered();
            TotalOrder();

            boolPickup = varpristrControlNumber.Pickup;
            SWPickup.IsToggled = boolPickup;
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong, possible error in connection", "OK");
            }
        }

        private async void TotalOrder()
        {
            try
            {
                double douTotalPaid = await App.ClsServeCompute.SumofTotalOrder();
                lblEntTotalAmt.Text = douTotalPaid.ToString("N2");
            }
            catch
            {
                lblEntTotalAmt.Text = "0.00";
            }
            finally
            {
            }
        }

        private async void TotalItemOrdered()
        {
            try
            {
                int intTotalItem = await App.ClsServeCompute.CountofTotalOrder();
                lblEntTotalItem.Text = intTotalItem.ToString();
            }
            catch
            {
                lblEntTotalItem.Text = "0";
            }
            finally
            {
            }
        }
        private async void IbtnShow_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageUnsavedOrders());
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(lblEntTotalItem.Text) == 0)
                {
                    await DisplayAlert("Information", "No item bought", "OK");
                }
                else
                {
                    string pristrGuid = Guid.NewGuid().ToString();
                    var varpristrControlNumber = await App.ClsServeList.GetCurrentCustomer();
                    string pristrControlNumber = varpristrControlNumber.ControlNumber.ToString();

                    var clientGet = new HttpClient();
                    clientGet.BaseAddress = new Uri(App.ClsServeList.GetIPAddress() + "/API/SpeedBuyWebAPI/SpeedBuyInsertData/InserttblMain1?strControlNumber=" + pristrControlNumber + "&strGuid=" + pristrGuid + "&intPickup=" + IntPickup);
                    HttpResponseMessage response = await clientGet.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        HttpClient client = new HttpClient();
                        var varloop = await App.ClsServeList.GetallOrder();
                        foreach (var VL in varloop)
                        {
                            intRowNum = intRowNum += 1;
                            var resultSub = await client.GetAsync(App.ClsServeList.GetIPAddress() + "/API/SpeedBuyWebAPI/SpeedBuyInsertData/InserttblMain2" +
                                "?strGuid=" + pristrGuid + "&strProductCode=" + VL.ProductCode + "&strPout=" + VL.POut + "&strProdSellingPrice=" + VL.SellingPrice +
                                "&strRowNum=" + intRowNum+ "&strUnitCost=" + VL.UnitCost);
                        }
                        var result = await client.GetAsync(App.ClsServeList.GetIPAddress() + "/API/SpeedBuyWebAPI/SpeedBuyEditData/FinalizeEncoding?strControlNumber=" + pristrControlNumber + "&strPKConnect=" + pristrGuid);
                        intRowNum = 0;
                        await App.ClsServeMain.db.DeleteAllAsync<ClsModelSO2>();
                        await DisplayAlert("Information", "Finished", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Information", "Record has not been added", "OK");
                    }
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong, possible error in connection", "OK");
            }
        }
        protected async override void OnAppearing()
        {
            try
            {

                var varpristrControlNumber = await App.ClsServeList.GetCurrentCustomer();
            pristrDeliveryAreaCode = varpristrControlNumber.DeliveryAreaCode.ToString();

            var varloop = await App.ClsServeWebAPI.GetProductListAll(pristrDeliveryAreaCode);
            var groupedData =
                varloop.OrderBy(p => p.WaitingTimeDesc)
                    //.GroupBy(p => p.PDFDeptDesc[0].ToString())
                    .GroupBy(p => p.WaitingTimeDesc.ToString())
                    .Select(p => new ObservableGroupCollection<string, ClsViewProductList>(p))
                    .ToList();
            BindingContext = new ObservableCollection<ObservableGroupCollection<string, ClsViewProductList>>(groupedData);
            TotalItemOrdered();
            TotalOrder();

            boolPickup = varpristrControlNumber.Pickup;
            SWPickup.IsToggled = boolPickup;
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong, possible error in connection", "OK");
            }
        }
    }
}