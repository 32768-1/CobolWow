﻿//using System.Linq;
//using System.Collections.Generic;

//using SQLite;
//using CobolWow.Tools.Database.Tables;

//namespace CobolWow.Tools.Database.Helpers
//{
//   //class DBActionBarButtons
//   //{
//   //   public static TableQuery<CharacterActionBarButton> CharacterActionBarButtonQuery
//   //   {
//   //      get { return DB.Character.Table<CharacterActionBarButton>(); }
//   //   }

//   //   public static TableQuery<CharacterCreationActionBarButton> CharacterCreationActionBarButtonQuery
//   //   {
//   //      get { return DB.World.Table<CharacterCreationActionBarButton>(); }
//   //   }

//   //   public static List<CharacterActionBarButton> GetActionBarButtons(Character character)
//   //   {

//   //      List<CharacterActionBarButton> result = CharacterActionBarButtonQuery.Where(cabb => cabb.GUID == character.GUID).ToList();
//   //      if (result.Count == 0)
//   //      {
//   //         List<CharacterCreationActionBarButton> newCharacterActionBarButtons = CharacterCreationActionBarButtonQuery.Where(ccabb => ccabb.Race == character.Race && ccabb.Class == character.Class).ToList();
//   //         newCharacterActionBarButtons.ForEach(ncabb =>
//   //         {
//   //            CharacterActionBarButton actionBarButton = new CharacterActionBarButton() { GUID = character.GUID, Action = ncabb.Action, Button = ncabb.Button, Type = ncabb.Type };
//   //            DB.Character.Insert(actionBarButton);
//   //            result.Add(actionBarButton);
//   //         });
//   //      }
//   //      return result;
//   //   }

//   //   public static CharacterActionBarButton GetActionBarButton(Character character, int action, int button, int type)
//   //   {
//   //      return CharacterActionBarButtonQuery.First(cabb => cabb.GUID == character.GUID && cabb.Action == action && cabb.Button == button && cabb.Type == type);
//   //   }

//   //   public static void AddActionBarButton(Character character, int action, int button, int type)
//   //   {
//   //      if (GetActionBarButton(character, action, button, type) == null)
//   //         DB.Character.Insert(new CharacterActionBarButton() { GUID = character.GUID, Action = action, Button = button, Type = type });
//   //   }

//   //   public static void RemoveActionBarButton(Character character, int action, int button, int type)
//   //   {
//   //      DB.Character.Delete(GetActionBarButton(character, action, button, type));
//   //   }
//   //}
//}
