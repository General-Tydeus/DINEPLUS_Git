using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using DINEPLUSBE.FldrServices;
using DINEPLUSBE.FldrControlPanel;
using DINEPLUSBE.FldrLoginPage;

namespace DINEPLUSBE
{
    public partial class App : Application
    {
        static ClsServeMain db;

        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new PageMainMenu());
            MainPage = new NavigationPage(new PageLogin());
        }
        public static ClsServeMain ClsServeMain
        {
            get
            {
                if (db == null)
                {
                    db = new ClsServeMain(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XamarinSQLite.db3"));
                }
                return db;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
