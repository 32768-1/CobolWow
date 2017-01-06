﻿using SQLite;

namespace CobolWow.Tools.Database.Tables
{
   public class Account
   {
      [PrimaryKey, AutoIncrement]
      public int ID { get; set; }

      public string Username { get; set; }
      public string Password { get; set; }
      public string SessionKey { get; set; }
   }
}
