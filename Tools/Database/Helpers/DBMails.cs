﻿//using System.Linq;
//using System.Collections.Generic;
//using CobolWow.Database20.Tables;
//using CobolWow.Tools.Database.Tables;
//using SQLite;

//namespace CobolWow.Tools.Database.Helpers
//{
//   public class DBMails
//   {
//      public static TableQuery<CharacterMail> MailQuery
//      {
//         get { return DB.Character.Table<CharacterMail>(); }
//      }

//      public static List<CharacterMail> GetCharacterMails(Character Character)
//      {
//         return MailQuery.ToList().FindAll(s => s.Reciever == Character.guid);
//      }

//      public static void AddMail(CharacterMail Mail)
//      {
//         DB.Character.Insert(Mail);
//      }

//      public static void AddMail()
//      {
//         AddMail(new CharacterMail() { });
//      }

//      public static void RemoveMail(CharacterMail Mail)
//      {
//         DB.Character.Delete(Mail);
//      }
//   }
//}
