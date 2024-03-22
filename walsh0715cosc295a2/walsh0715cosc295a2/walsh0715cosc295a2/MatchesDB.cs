using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace walsh0715cosc295a2
{
    public class MatchesDB
    {
        readonly SQLiteConnection database;
        public MatchesDB(string dbPath)
        {
            database = new SQLiteConnection(dbPath);

            if (database.Table<Match>().Count() == 0)    // Table<> returns a collection we can do further operations on, like Count or Where
            {
                // configure and save a new purchase
                Match match = new Match { oppID = 1, date = DateTime.Now, comments = "Good game friendo", gameID = 1, win = true};
                SaveMatch(match);
            }
        }

        public int SaveMatch(Match match)
        {
            if (match.ID != 0)
            {
                return database.Update(match);   // perform an update on the associated record
            }
            else
            {
                return database.Insert(match);   // returns primary key that was generated
            }
        }
        public int DeleteMatch(Match match)
        {
            return database.Delete(match);
        }
        public List<Match> GetMatches()
        {
            return database.Table<Match>().ToList<Match>();
        }
        public Match GetMatch(int id)
        {
            return database.Table<Match>().Where(i => i.ID == id).FirstOrDefault();  // 
        }
    }
}
