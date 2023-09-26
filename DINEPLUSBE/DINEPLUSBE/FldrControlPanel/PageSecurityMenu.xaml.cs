
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrEntry;
using DINEPLUSBE.FldrLoginPage;
using DINEPLUSBE.FldrSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DINEPLUSBE.FldrControlPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSecurityMenu : ContentPage
    {
       
        public PageSecurityMenu()
        {
            InitializeComponent();
        }

        private async void BtnUserAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageUserAdd());
        }
    }
}