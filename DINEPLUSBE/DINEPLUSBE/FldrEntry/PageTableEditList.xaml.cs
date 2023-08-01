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
	public partial class PageTableEditList : ContentPage
	{
        public PageTableEditList()
		{
			InitializeComponent ();
            LVTableList.ItemSelected += LVTableList_ItemSelected;
		}

        private  void LVTableList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    Navigation.PushAsync(new PageTableEdit()
                    {
                        BindingContext = e.SelectedItem as ModeltblTable
                    });
                }
            }
            catch (Exception)
            {
                DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

        protected async override void OnAppearing()
        {
            try
            {
                LVTableList.ItemsSource = await new ClsList().GetTable();
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ModeltblTable> ModeltblTable1;
            ModeltblTable1 = await new ClsList().GetTable();
            LVTableList.ItemsSource = ModeltblTable1.Where(x => x.TableDesc.ToLower().Contains(e.NewTextValue)).ToList();
        }

    }
}