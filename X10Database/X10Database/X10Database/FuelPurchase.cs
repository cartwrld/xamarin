using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace X10Database
{
    public class FuelPurchase
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Litres { get; set; }
        public double Cost { get; set; }
    }
}
