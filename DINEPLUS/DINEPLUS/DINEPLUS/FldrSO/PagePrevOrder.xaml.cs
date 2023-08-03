using DINEPLUS.FldrModel;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePrevOrder : ContentPage
    {
        MdlTables mdlTables11;
        List<LocaltblMain2> PreviousOrd;

        public PagePrevOrder(MdlTables mdlTables1)
        {
            mdlTables11 = mdlTables1;
            InitializeComponent();
            BindingContext = mdlTables1;
        }


        protected async override void OnAppearing()
        {
            PreviousOrd = await App.ClsServeMain.LocaltblMainTwo(mdlTables11.TableDocNum);
            LV1.ItemsSource = PreviousOrd;
            lblTotals.Text = $"₱ {PreviousOrd.Sum(x => x.Totals).ToString("N2")}";
        }

    }
}