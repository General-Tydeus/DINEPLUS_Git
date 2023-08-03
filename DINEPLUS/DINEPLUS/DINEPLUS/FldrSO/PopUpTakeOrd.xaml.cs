using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
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
            App.ClsServeMain.TableOccupied(tblSavetblMain1().TableCode);

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
                UserCode = PageLogin.glbltxtUserCode,
                TDate = DateTime.Now,
                Reference = $"SO{MyDocNum}",
                ControlNo = "123",
                Remarks = txtRemarks.Text,
                CNCode = PageLogin.glbltxtCNCode,
                TableCode = PageProductList.Instance.MdlTables11.TableCode,
                TableDesc = PageProductList.Instance.MdlTables11.TableDesc,
                CAmount = PageSOCart.Instance.Totals
            };
        }
    }
}