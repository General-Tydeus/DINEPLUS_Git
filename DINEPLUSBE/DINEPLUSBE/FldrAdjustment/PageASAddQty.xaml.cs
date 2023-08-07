using DINEPLUSBE.FldrModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUSBE.FldrAdjustment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageASAddQty : ContentPage
    {
        private string strHeadStockNumber, strHeadSProductDesc, strHeadUnitM, strHeadSellingPrice, strHeadUCost;
        private int priintRowNum = 1;


        public PageASAddQty(string strInitStockNumber, string strInitSProductDesc, string strInitUnitM, string strInitSellingPrice, string strInitUCost)
        {
            InitializeComponent();
            strHeadStockNumber = strInitStockNumber;
            strHeadSProductDesc = strInitSProductDesc;
            strHeadUnitM = strInitUnitM;
            strHeadSellingPrice = strInitSellingPrice;
            strHeadUCost = strInitUCost;
            txtPIn.Text = "1.00";
            txtPOut.Text = "0.00";
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
            if (double.Parse(txtPIn.Text)+double.Parse(txtPOut.Text) == 0)
            {
                await DisplayAlert("Information", "Zero quantity", "OK");
                txtPIn.Focus();
            }
            else
            {
                if (PageASProductSearchList.Instance.ModeltblMain2ASList.Count == 0)
                {
                    priintRowNum = 1;
                }
                else
                {
                    var varTopRowNum = PageASProductSearchList.Instance.ModeltblMain2ASList.Max(i => i.RowNum);
                    priintRowNum = varTopRowNum + 1;
                }
                ModeltblMain2AS ModeltblMain2AS1 = new ModeltblMain2AS()
                {
                    StockNumber = lblStockNumber.Text,
                    ProductDesc = lblProductDesc.Text,
                    PIn = double.Parse(txtPIn.Text),
                    POut = double.Parse(txtPOut.Text),
                    Qty=double.Parse(txtPIn.Text)-double.Parse(txtPOut.Text),
                    UCost = double.Parse(txtUnitCost.Text),
                    RowNum = priintRowNum,
                    Total=(double.Parse(txtPIn.Text)-double.Parse(txtPOut.Text))*double.Parse(txtUnitCost.Text),
                };
            PageASProductSearchList.Instance.ModeltblMain2ASList.Add(ModeltblMain2AS1);
            await Navigation.PopAsync();
            }
        }
        private string TotalAmt()
        {
            string strTotal = ((double.Parse(txtPIn.Text)-double.Parse(txtPOut.Text)) * double.Parse(txtUnitCost.Text)).ToString("N2");
            return strTotal;
        }
     }
}