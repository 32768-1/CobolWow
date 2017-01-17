using CobolWow.Database20.Tables;
using CobolWow.Tools;
using CobolWow.Tools.Database.Tables;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CobolWow.Database20.Accounts
{
   public static class RealmAccountManager
   {
      public static Account GetAccount(String username)
      {
         return DataBase20.RealmDatabase.Query<Account>("Select * From account").FirstOrDefault(a => a.username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
      }

      public static void CreateAccount(String username, String password)
      {
         //DB.Character.Insert(new Account() { Username = username, Password = password });
      }

      public static void UpdateAccount(Account account)
      {
         DataBase20.RealmDatabase.Update<Account>(account);
      }

      //public static void SetSessionKey(String username, String sessionKey)
      //{
      //   Account account = GetAccount(username);
      //   account.sessionkey = Regex.Replace(sessionKey, @"\s+", "");

      //   var ses = Regex.Replace(sessionKey, @"\s+", "");
      //   DataBase20.RealmDatabase.Update<Account>(account);
      //}
   }
}
