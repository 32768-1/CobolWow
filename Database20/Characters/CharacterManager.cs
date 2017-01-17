using CobolWow.Database20.Tables;
using CobolWow.Game.Constants;
using CobolWow.Tools;
using CobolWow.Tools.Database20.Tables;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CobolWow.Database20.Characters
{
   public static class CharacterManager
   {
      private static HashSet<CharacterCreationInfo> CharacterCreationInfo;
      private static HashSet<Character> Characters;
      public async static void Initialize()
      {
         Logger.Log(LogType.Database, "Loading Characters...");
         var _characters = await DataBase20.CharacterDatabase.QueryAsync<Character>("Select * From characters");
         Characters = new HashSet<Character>(_characters);
         Logger.Log(LogType.Database, string.Format("Loaded {0} Characters...", Characters.Count));

         Logger.Log(LogType.Database, "Loading CharacterCreationInfo...");
         var _creationInfo = await DataBase20.WorldDatabase.QueryAsync<CharacterCreationInfo>("Select * From playercreateinfo");
         CharacterCreationInfo = new HashSet<CharacterCreationInfo>(_creationInfo);
         Logger.Log(LogType.Database, string.Format("Loaded {0} CharacterCreationInfo...", CharacterCreationInfo.Count));
      }

      public static CharacterCreationInfo GetCreationInfo(RaceID raceID, ClassID classID)
      {
         return CharacterCreationInfo.First(m => m.Race == raceID && m.Class == classID);
      }

      public static List<Character> GetCharacters(String username)
      {
         int accountID = (int)Accounts.RealmAccountManager.GetAccount(username).id;
         Logger.Log(LogType.Debug, "Account ID retrieved: " + accountID);
         return Characters.Where(a => a.account == accountID).ToList();
      }

      public static Character GetCharacter(string name)
      {
         return Characters.Where(c => String.Equals(c.name, name, StringComparison.CurrentCultureIgnoreCase)).First();
      }

      public static Character GetCharacter(int guid)
      {
         return Characters.Where(c => c.guid == guid).First();
      }

      public static void CreateCharacter(Account owner, Character character)
      {
         character.account = (int)owner.id;

         DataBase20.CharacterDatabase.Execute("insert characters(account,name,race,class,gender,level,xp,money,playerBytes,playerBytes2,playerFlags,position_x,position_y,position_z,map,orientation,taximask,online,cinematic,totaltime,leveltime,logout_time,is_logout_reseting,reset_bonus,resettalents_cost,resettalents_time,trans_x,trans_y,trans_z,trans_o,transguid,extra_flags,stable_slots,at_login,zone,death_expire_time,taxi_path,honor_highest_rank,honor_standing,stored_honor,rating,stored_dishonorable_kills,stored_honorable_kills,watchedFaction,drunk,health,power1,power2,power3,power4,power5,exploredZones,equipmentCache,ammoId,actionBars) values(" + string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53})",
                                      owner.id,
                                      character.name,
                                      character.race,
                                      character.@class,
                                      character.gender,
                                      character.level,
                                      character.xp,
                                      character.money,
                                      character.playerints,
                                      character.playerints2,
                                      character.playerFlags,
                                      character.position_x,
                                      character.position_y,
                                      character.position_z,
                                      character.map,
                                      character.orientation,
                                      character.taximask,
                                      character.online,
                                      character.cinematic,
                                      character.totaltime,
                                      character.leveltime,
                                      character.logout_time,
                                      character.is_logout_resting,
                                      character.rest_bonus,
                                      character.resettalents_cost,
                                      character.resettalents_time,
                                      character.trans_x,
                                      character.trans_y,
                                      character.trans_z,
                                      character.trans_o,
                                      character.transguid,
                                      character.extra_flags,
                                      character.stable_slots,
                                      character.at_login,
                                      character.zone,
                                      character.death_expire_time,
                                      character.taxi_path,
                                      character.honor_highest_rank,
                                      character.honor_standing,
                                      character.stored_honor_rating,
                                      character.stored_dishonorable_kills,
                                      character.stored_honorable_kills,
                                      character.watchedFaction,
                                      character.drunk,
                                      character.health,
                                      character.power1,
                                      character.power2,
                                      character.power3,
                                      character.power4,
                                      character.power5,
                                      "",
                                      "6140,1395,6096,55,35,117,159,9575,6948,0,0,0",//character.equipmentCache, TODO!
                                      0,
                                      character.actionBars));
      }

      public static void DeleteCharacter(Character character)
      {
         //DB.Character.Delete(character);
         //Database20.Characters.ActionButtonsManager.GetActionBarButtons(character).ForEach(b => DB.Character.Delete(b));
         //DBSpells.GetCharacterSpells(character).ForEach(s => DB.Character.Delete(s));
         //DBChannels.GetChannelCharacters(character).ForEach(c => DB.Character.Delete(c));
      }

      public static void UpdateCharacter(Character character)
      {
         //DB.Character.Update(character);
      }
   }
}
