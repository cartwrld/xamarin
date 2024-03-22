using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace walsh0715cosc295a2
{
    public class GamesDB
    {
        readonly SQLiteConnection database;

        public GamesDB(string dbPath)
        {
            database = new SQLiteConnection(dbPath);

            if (database.Table<Game>().Count() == 0)    // Table<> returns a collection we can do further operations on, like Count or Where
            {
                // configure and save a new purchase
                Game game = new Game { gameName = "Overwatch", description = "Hanamura", rating = 9.5 };
                SaveGame(game);
            }
        }

        public int SaveGame(Game game)
        {
            if (game.ID != 0)
            {
                return database.Update(game);   // perform an update on the associated record
            }
            else
            {
                return database.Insert(game);   // returns primary key that was generated
            }
        }
        public int DeleteGame(Game game)
        {
            return database.Delete(game);
        }
        public List<Game> GetGames()
        {
            return database.Table<Game>().ToList<Game>();
        }
        public Game GetGame(int id)
        {
            return database.Table<Game>().Where(i => i.ID == id).FirstOrDefault();  // 
        }

    }
}
