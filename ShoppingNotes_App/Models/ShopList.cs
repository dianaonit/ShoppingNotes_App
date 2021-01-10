using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ShoppingNotes_App.Models
{
    public class ShopList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } = -1;
        public bool Exists { get => ID != -1; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

    }
}
