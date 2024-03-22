using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace walsh0715cosc295a2
{
    public class Match
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int oppID { get; set; }
        public DateTime date { get; set; }
        public string comments { get; set; }
        public int gameID { get; set; }
        public bool win { get; set; }


    }
}
