using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace walsh0715cosc295a2
{
    public partial class App : Application
    {
        static OpponentsDB oppDB;
        static MatchesDB matchDB;
        static GamesDB gameDB;

        public static OpponentsDB OppDatabase     // this is helpful in other pages, as App.Database is now available in other pages.
        {
            get
            {
                if (oppDB == null)   // check if database is already connected
                {
                    oppDB = new OpponentsDB(DependencyService.Get<IFileHelper>().GetLocalFilePath("OpponentsSQLite.db3"));
                }
                return oppDB;
            }
        }
        public static MatchesDB MatchesDatabase
        {
            get
            {
                if (matchDB == null)   // check if database is already connected
                {
                    matchDB = new MatchesDB(DependencyService.Get<IFileHelper>().GetLocalFilePath("MatchesSQLite.db3"));
                }
                return matchDB;
            }
        }
        public static GamesDB GamesDatabase
        {
            get
            {
                if (gameDB == null)   // check if database is already connected
                {
                    gameDB = new GamesDB(DependencyService.Get<IFileHelper>().GetLocalFilePath("GamesSQLite.db3"));
                }
                return gameDB;
            }
        }

        public App()
        {
            InitializeComponent();

            //List<Opponent> oppList = App.OppDatabase.GetOpponents();


            MainPage = new NavigationPage(new OpponentsPage());
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

    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }

}
