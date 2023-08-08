using DINEPLUS.FldrClass;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrServices;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DINEPLUS
{
    public partial class App : Application
    {
        static ClsServeMain db;
        static ClsServeInsertLocal dbInsertLocal;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PageMainMenu());
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


        public static ClsServeInsertLocal ClsServeInsertLocal
        {
            get
            {
                if (dbInsertLocal == null)
                {
                    dbInsertLocal = new ClsServeInsertLocal(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XamarinSQLite.db3"));
                }
                return dbInsertLocal;
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
