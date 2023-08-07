using DINEPLUS.FldrClass;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrServices;
using System;
using System.IO;
using Xamarin.Forms;

namespace DINEPLUS
{
    public partial class App : Application
    {
        static ClsServeMain db;
        public App()
        {
            InitializeComponent();

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
