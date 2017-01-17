using Dapper;
using System.Linq;
using CobolWow.Tools;
using System.Threading.Tasks;
using System.Collections.Generic;
using CobolWow.Database20.Tables;

namespace CobolWow.Database20.Characters
{
   public static class ActionButtonsManager
   {
      private static HashSet<CharacterActionBarButton> ActionButtons;
      private static HashSet<CharacterCreationActionBarButton> CreationActionBarButtons;
      public async static void Initialize()
      {
         Logger.Log(LogType.Database, "Loading CharacterActionBarButton...");
         var action_buttons = await DataBase20.CharacterDatabase.QueryAsync<CharacterActionBarButton>("Select * From character_action");
         ActionButtons = new HashSet<CharacterActionBarButton>(action_buttons);
         Logger.Log(LogType.Database, string.Format("Loaded {0} ActionButtons...", ActionButtons.Count));

         var creation_action_buttons = await DataBase20.WorldDatabase.QueryAsync<CharacterCreationActionBarButton>("Select * From playercreateinfo_action");
         CreationActionBarButtons = new HashSet<CharacterCreationActionBarButton>(creation_action_buttons);

         Logger.Log(LogType.Database, string.Format("Loaded {0} CharacterActionBarButton...", CreationActionBarButtons.Count));
      }

      public static List<CharacterActionBarButton> GetActionBarButtons(Character character)
      {
         var result = ActionButtons.Where(cabb => cabb.guid == character.guid).ToList();
         if (result.Count == 0)
         {
            List<CharacterCreationActionBarButton> _newBarButtons = CreationActionBarButtons.Where(ccabb => ccabb.race == character.race && ccabb.@class == character.@class).ToList();
            foreach (CharacterCreationActionBarButton _button in _newBarButtons)
            {
               CharacterActionBarButton actionBarButton = new CharacterActionBarButton() { guid = character.guid, action = _button.action, button = _button.button, type = _button.type };
               
               DataBase20.CharacterDatabase.QueryAsync(string.Format("insert playercreateinfo_action(guid,action,button,type) values({0},{1},{2},{3})", actionBarButton.guid, actionBarButton.action, actionBarButton.button, actionBarButton.type));
               ActionButtons.Add(actionBarButton); //Add this new element to our cached hash.
               result.Add(actionBarButton);
            }
         }
         return result;
      }

      public async static Task<CharacterActionBarButton> GetActionBarButton(Character character, int action, int button, int type)
      {
         CharacterActionBarButton _chrBtn = null;
         await Task.Run(() =>
         {
            _chrBtn = ActionButtons.First(cabb => cabb.guid == character.guid && cabb.action == action && cabb.button == button && cabb.type == type);
         });
         return _chrBtn;
      }

      //public static void AddActionBarButton(Character character, int action, int button, int type)
      //{
      //   if (GetActionBarButton(character, action, button, type) == null)
      //      DB.Character.Insert(new CharacterActionBarButton() { GUID = character.GUID, Action = action, Button = button, Type = type });
      //}

      //public static void RemoveActionBarButton(Character character, int action, int button, int type)
      //{
      //   DB.Character.Delete(GetActionBarButton(character, action, button, type));
      //}
   }
}
