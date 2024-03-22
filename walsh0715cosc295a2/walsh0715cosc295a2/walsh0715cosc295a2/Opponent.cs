using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace walsh0715cosc295a2
{
    public class Opponent
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
