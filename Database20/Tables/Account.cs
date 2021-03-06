﻿using Dapper.Contrib.Extensions;

namespace CobolWow.Database20.Tables
{
   [Table("account")]
   public class Account
   {
      public long id { get; set; }
      public string username { get; set; }
      public string sha_pass_hash { get; set; }
      public byte gmlevel { get; set; }
      public string sessionkey { get; set; }
      public string v { get; set; }
      public string s { get; set; }
      public string email { get; set; }
      public System.DateTime joindate { get; set; }
      public string last_ip { get; set; }
      public long failed_logins { get; set; }
      public byte locked { get; set; }
      public System.DateTime last_login { get; set; }
      public long active_realm_id { get; set; }
      public byte expansion { get; set; }
      public decimal mutetime { get; set; }
      public byte locale { get; set; }
   }
}
