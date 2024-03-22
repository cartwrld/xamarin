using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace walsh0715cosc295a2
{
    public class OpponentsDB
    {
        readonly SQLiteConnection database;
        public OpponentsDB(string dbPath)
        {
            database = new SQLiteConnection(dbPath);

            database.CreateTable<Opponent>();

            if (database.Table<Opponent>().Count() == 0)    // Table<> returns a collection we can do further operations on, like Count or Where
            {
                // configure and save a new purchase
                Opponent opp = new Opponent { fname = "Brett", lname = "Slobodzian", address = "123 Wallaby Way", phone = "123-456-7890", email="brett@slobs.net" };
                SaveOpponent(opp);
            }
        }

        public int SaveOpponent(Opponent opp)
        {
            if (opp.ID != 0)
            {
                return database.Update(opp);   // perform an update on the associated record
            }
            else
            {
                return database.Insert(opp);   // returns primary key that was generated
            }
        }
        public int DeleteOpponent(Opponent opp)
        {
            return database.Delete(opp);
        }
        public List<Opponent> GetOpponents()
        {
            return database.Table<Opponent>().ToList<Opponent>();
        }
        public Opponent GetOpponent(int id)
        {
            return database.Table<Opponent>().Where(i => i.ID == id).FirstOrDefault(); 
        }
    }
}

