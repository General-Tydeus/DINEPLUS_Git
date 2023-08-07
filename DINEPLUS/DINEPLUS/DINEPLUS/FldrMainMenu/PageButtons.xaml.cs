using DINEPLUS.FldrClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageButtons : ContentView
    {
        public PageButtons()
        {
            InitializeComponent();
        }

        private async void btnSO_Clicked(object sender, EventArgs e)
        {
            var targetPage = ((App)Application.Current).MainPage.Navigation.NavigationStack.FirstOrDefault(p => p.GetType() == typeof(PageSO));

            if (targetPage != null)
            {
                ((App)Application.Current).MainPage.Navigation.RemovePage(targetPage);
            }

            await ((App)Application.Current).MainPage.Navigation.PushAsync(new PageSO());
        }

        private async void btnPayo_Clicked(object sender, EventArgs e)
        {
            var targetPage = ((App)Application.Current).MainPage.Navigation.NavigationStack.FirstOrDefault(p => p.GetType() == typeof(PagePAYO));

            if (targetPage != null)
            {
                ((App)Application.Current).MainPage.Navigation.RemovePage(targetPage);
            }

            await ((App)Application.Current).MainPage.Navigation.PushAsync(new PagePAYO());

        }

    }
}