using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpTakeOrd : PopupPage
    {
        public string MyDocNum { get; set; }
        public string MyReference { get; set; }



        public PopUpTakeOrd()
        {
            InitializeComponent();
            lblTotals.Text = $"₱ {PageSOCart.Instance.Totals.ToString("n2")}";
        }
        protected async override void OnAppearing()
        {
            int SOMainCount1 = await App.ClsServeMain.SOMainCount();
            SOMainCount1++;
            MyDocNum = Convert.ToString(SOMainCount1).PadLeft(7, '0');
        }

        private void btnClose_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private async void BtnPost_Clicked(object sender, System.EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txtRemarks.Text))
            {
                await DisplayAlert("info","please complete entry!","OK");
                return;
            }

            await App.ClsServeMain.SaveClsModelSO1(tblSavetblMain1());
            await App.ClsServeMain.SaveClsModelSO2(SavetblMain2());

            App.ClsServeMain.TableOccupied(tblSavetblMain1().TableCode, MyDocNum);

            PageSOCart.Instance.clsPage2();
            await PageSO.Instance.LoadSumary();
            await Navigation.PopPopupAsync();
        }


        public LocaltblMain1 tblSavetblMain1()
        {
            return new LocaltblMain1()
            {
                IC = $"SO{MyDocNum}",
                Voucher = "SO",
                DocNum = MyDocNum,
                UserCode = PageMainMenu.Instance.strUserCode,
                TDate = DateTime.Now,
                Reference = $"SO{MyDocNum}",
                ControlNo = "123",
                Remarks = txtRemarks.Text,
                CNCode = PageMainMenu.Instance.strCNCode,
                TableCode = PageProductList.Instance.MdlTables11.TableCode,
                TableDesc = PageProductList.Instance.MdlTables11.TableDesc,
                CAmount = PageSOCart.Instance.Totals
            };
        }



        public List<LocaltblMain2> SavetblMain2()
        {
            var listofData = new List<LocaltblMain2>();
            foreach (var vl in PageProductList.Instance.listOrders)
            {
                listofData.Add(new LocaltblMain2()
                {
                    IC = $"SO{MyDocNum}",
                    DocNum = MyDocNum,
                    StockNumber = vl.StockNumber,
                    PIn = 0,
                    POut = vl.Qty,
                    UP = vl.SellingPrice,
                    Cost = vl.UCost,
                    Discount = 0,
                    ProductDesc = vl.ProductDesc,
                    Totals = vl.Totals,
                    OrderTime = DateTime.Now.ToString("hh:mm tt")
                });
            }
            return listofData;
        }
    }
}