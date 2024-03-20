using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X10Database
{
    public class FuelDatabase
    {
        readonly SQLiteConnection database;
        public FuelDatabase(string dbPath)
        {
            database = new SQLiteConnection(dbPath);    // attempt to either create the db file, or open an existing file
            database.CreateTable<FuelPurchase>();   // will make the table for Fuel Purchases if it doesn't already exist

            // create a dummy row just so we have some data, normally not done in a real app

            if (database.Table<FuelPurchase>().Count() == 0)    // Table<> returns a collection we can do further operations on, like Count or Where
            {
                // configure and save a new purchase
                FuelPurchase purchase = new FuelPurchase { Cost=15, Litres=10, Date = new DateTime(2024, 3, 15) };
                SaveItem(purchase);
            }

        }

        public int SaveItem(FuelPurchase item)
        {
            // check if this is an existing item

            if (item.ID != 0)
            {
                return database.Update(item);   // perform an update on the associated record
            }
            else
            {
                return database.Insert(item);   // returns primary key that was generated
            }
        }

        public int DeleteItem(FuelPurchase item)
        {
            return database.Delete(item);
        }

        public List<FuelPurchase> GetItems() 
        {
            return database.Table<FuelPurchase>().ToList<FuelPurchase>();   // create a list containing all rows/objects
        }

        public FuelPurchase GetItem(int id)
        {
            return database.Table<FuelPurchase>().Where(i => i.ID == id).FirstOrDefault();  // returns the purchase that matches the passed in ID using LINQ
        }

        public List<FuelPurchase> GetItemsOverTenLiters()
        {
            // adhoc queries can be performed with the Query<>() method
            return database.Query<FuelPurchase>("SELECT * FROM [FuelPurchase] WHERE [Litres] > 10");    // returns a regular list
        }


    }
}
