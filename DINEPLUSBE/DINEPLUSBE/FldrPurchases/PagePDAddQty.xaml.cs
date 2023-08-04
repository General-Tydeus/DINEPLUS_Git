using DINEPLUSBE.FldrModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUSBE.FldrPurchases
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePDAddQty : ContentPage
    {
        private string strHeadStockNumber, strHeadSProductDesc, strHeadUnitM, strHeadSellingPrice, strHeadUCost;
        private int priintRowNum = 1;


        public PagePDAddQty(string strInitStockNumber, string strInitSProductDesc, string strInitUnitM, string strInitSellingPrice, string strInitUCost)
        {
            InitializeComponent();
            strHeadStockNumber = strInitStockNumber;
            strHeadSProductDesc = strInitSProductDesc;
            strHeadUnitM = strInitUnitM;
            strHeadSellingPrice = strInitSellingPrice;
            strHeadUCost = strInitUCost;
            txtQty.Text = "0.00";
            lblEntTotal.Text = "0.00";
            txtUnitCost.Text = double.Parse(strHeadUCost).ToString("N2");

            lblStockNumber.Text = strHeadStockNumber;
            lblProductDesc.Text = strHeadSProductDesc;
            lblEntUM.Text = strHeadUnitM;
            txtUnitCost.Text = strHeadUCost;
        }
        private void txtQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lblEntTotal.Text = TotalAmt();
            }
            catch (Exception)
            {
                lblEntTotal.Text = "0.00";
            }
        }

        private void txtUnitCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lblEntTotal.Text = TotalAmt();
            }
            catch (Exception)
            {
                lblEntTotal.Text = "0.00";
            }
        }
        private async void BtnPost_Clicked(object sender, EventArgs e)
        {
            if (double.Parse(txtQty.Text) == 0)
            {
                await DisplayAlert("Information", "Zero quantity", "OK");
                txtQty.Focus();
            }
            else
            {
                if (PagePDProductSearchList.Instance.ModeltblMain2PDList.Count == 0)
                {
                    priintRowNum = 1;
                }
                else
                {
                    var varTopRowNum = PagePDProductSearchList.Instance.ModeltblMain2PDList.Max(i => i.RowNum);
                    priintRowNum = varTopRowNum + 1;
                }
                ModeltblMain2PD ModeltblMain2PD1 = new ModeltblMain2PD()
                {
                    StockNumber = lblStockNumber.Text,
                    ProductDesc = lblProductDesc.Text,
                    PIn = double.Parse(txtQty.Text),
                    UCost = double.Parse(txtUnitCost.Text),
                    RowNum = priintRowNum,
                    Total=double.Parse(txtQty.Text)*double.Parse(txtUnitCost.Text),
                };
            PagePDProductSearchList.Instance.ModeltblMain2PDList.Add(ModeltblMain2PD1);
            await Navigation.PopAsync();
            }
        }
        private string TotalAmt()
        {
            string strTotal = (double.Parse(txtQty.Text) * double.Parse(txtUnitCost.Text)).ToString("N2");
            return strTotal;
        }
     }
}