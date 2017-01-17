//using System.Linq;
//using System.Collections.Generic;

//using SQLite;
//using CobolWow.Database20.Tables;
//using CobolWow.Tools.Database.Tables;

//namespace CobolWow.Tools.Database.Helpers
//{
//   class DBSpells
//   {
//      public static TableQuery<CharacterSpell> CharacterSpellQuery
//      {
//         get { return DB.Character.Table<CharacterSpell>(); }
//      }

//      public static TableQuery<CharacterCreationSpell> CharacterCreationSpellQuery
//      {
//         get { return DB.World.Table<CharacterCreationSpell>(); }
//      }

//      public static List<CharacterSpell> GetCharacterSpells(Character character)
//      {

//         List<CharacterSpell> result = CharacterSpellQuery.ToList().FindAll(s => s.GUID == character.guid);
//         if (result.Count == 0)
//         {
//            List<CharacterCreationSpell> newCharacterSpells = CharacterCreationSpellQuery.Where(s => s.Race == character.race && s.Class == character.@class).ToList();
//            newCharacterSpells.ForEach(s =>
//                {
//                   CharacterSpell newSpell = new CharacterSpell() { GUID = character.guid, SpellID = s.SpellID };
//                   DB.Character.Insert(newSpell);
//                   result.Add(newSpell);
//                });
//         }
//         return result;
//      }

//      public static CharacterSpell GetCharacterSpell(Character character, int spellID)
//      {
//         return CharacterSpellQuery.First(s => s.GUID == character.guid && s.SpellID == spellID);
//      }

//      public static void AddSpell(Character character, int spellID)
//      {
//         DB.Character.Insert(new CharacterSpell() { GUID = character.guid, SpellID = spellID });
//      }

//      public static void RemoveSpell(Character character, int spellID)
//      {
//         DB.Character.Delete(GetCharacterSpell(character, spellID));
//      }
//   }
//}
