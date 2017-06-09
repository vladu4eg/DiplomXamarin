using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace diplom_mob1
{
    public partial class App : Application
    {

        public const string DATABASE_NAME = "NameId.db";
        public static NameIdReposirory database;
        public static NameIdReposirory Database
        {
            get
            {
                if (database == null)
                {
                    database = new NameIdReposirory(DATABASE_NAME);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
