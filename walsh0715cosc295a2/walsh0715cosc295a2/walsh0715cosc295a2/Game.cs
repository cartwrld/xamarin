using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace walsh0715cosc295a2
{
    public class Game
    { 
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string gameName { get; set; }
        public string description { get; set; }
        public double rating { get; set; }

    }
}
