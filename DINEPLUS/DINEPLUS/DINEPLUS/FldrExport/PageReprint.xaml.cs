using Acr.UserDialogs;
using DINEPLUS.FldrModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrExport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageReprint : TabbedPage
    {
        public static PageReprint Instance;

        public List<tblMain1Local> localMain1 = new List<tblMain1Local>();
        public List<tblMain1Local> localMain12 = new List<tblMain1Local>();
        HashSet<string> insertMain1 = new HashSet<string>();
        HashSet<string> insertMain12 = new HashSet<string>();
        public string findDocnum { get; set; }
        public string voucher { get; set; }

        public List<tblMain2Local> localMain2;
        public List<tblMain2Local> localMain22;
        NetworkAccess current;
        public PageReprint()
        {
            InitializeComponent();
            LoadData();
            Instance = this;
        }
        public async void LoadData()
        {
            await LoadLV();
            await LoadLV2();
        }
        public async Task LoadLV()
        {
            localMain1.Clear();
            localMain2 = await App.ClsServeMain.localMain2Reprint();

            foreach (var varlist in localMain2)
            {
                if (!insertMain1.Contains(varlist.DocNumLocal))
                {
                    var main2 = await App.ClsServeMain.localMain1RP(varlist.DocNumLocal, "CS");
                    localMain1.AddRange(main2);

                insertMain1.Add(varlist.DocNumLocal);
            }
        }
            LV1.ItemsSource = null;
            LV1.ItemsSource = localMain1;
        }
        public async Task LoadLV2()
        {
            localMain12.Clear();
            localMain22 = await App.ClsServeMain.localMain2Reprint();

            foreach (var varlist in localMain22)
            {
                if (!insertMain12.Contains(varlist.DocNumLocal))
                {
                    var main2 = await App.ClsServeMain.localMain1RP(varlist.DocNumLocal, "SO");
                    localMain12.AddRange(main2);

                    insertMain12.Add(varlist.DocNumLocal);
                }
            }
            LV2.ItemsSource = null;
            LV2.ItemsSource = localMain12;
        }

        private async void LV1_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is tblMain1Local docnum)
            {
                await Navigation.PushAsync(new PageRP(docnum.DocNum, docnum.OrderTime, docnum.Discount, docnum.CashReceived, docnum.UserCode, docnum.TDate));
            }

            if (sender is ListView listView)
                listView.SelectedItem = null;
        }

        private async void LV2_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is tblMain1Local docnum)
            {
                await Navigation.PushAsync(new PageRP2(docnum.DocNum, docnum.OrderTime, docnum.Discount, docnum.CashReceived, docnum.UserCode, docnum.TDate, docnum.TableDesc));
            }

            if (sender is ListView listView)
                listView.SelectedItem = null;
        }
    }
}