using System;
using Dapper;
using System.Linq;
using CobolWow.Tools;
using System.Collections.Generic;
using CobolWow.Database20.Tables;
using CobolWow.Game.Constants.Game.World.Item;

namespace CobolWow.Database20.Items
{
   public static class ItemManager
   {
      private static Dictionary<int, ItemTemplateEntry> ItemTemplates = new Dictionary<int, ItemTemplateEntry>();

      public async static void Initialize()
      {
         Logger.Log(LogType.Database, "Loading ItemTemplateEntrys...");
         var items = await DataBase20.WorldDatabase.QueryAsync<ItemTemplateEntry>("Select * From item_template");
         ItemTemplates = items.ToDictionary(t => t.entry, i => i);
         Logger.Log(LogType.Database, string.Format("Loaded {0} ItemTemplateEntrys...", ItemTemplates.Count));
      }

      public static ItemTemplateEntry GetItemTemplateEntry(int id)
      {
         if (ItemTemplates.ContainsKey(id))
            return ItemTemplates[id];
         else
            throw new Exception("Unable to locate item with ID: " + id);
      }

      public static ItemTemplateEntry GetItemByName(String name)
      {
         return ItemTemplates.Where(i => i.Value.name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault().Value;
      }


      public static ItemTemplateEntry[] GenerateInventoryByIDs(int[] ids)
      {
         ItemTemplateEntry[] inventory = new ItemTemplateEntry[19];
         for (int i = 0; i < ids.Length; i++)
         {
            if (ids[i] > 0)
            {
               ItemTemplateEntry item = ItemManager.GetItemTemplateEntry(ids[i]);

               switch ((InventorySlotID)item.InventoryType)
               {
                  case InventorySlotID.INVTYPE_HEAD:
                     inventory[0] = item;
                     break;
                  case InventorySlotID.INVTYPE_BODY:
                     inventory[3] = item;
                     break;
                  case InventorySlotID.INVTYPE_CHEST:
                  case InventorySlotID.INVTYPE_ROBE:
                     inventory[4] = item;
                     break;
                  case InventorySlotID.INVTYPE_WAIST:
                     inventory[5] = item;
                     break;
                  case InventorySlotID.INVTYPE_LEGS:
                     inventory[6] = item;
                     break;
                  case InventorySlotID.INVTYPE_FEET:
                     inventory[7] = item;
                     break;
                  case InventorySlotID.INVTYPE_WRISTS:
                     inventory[8] = item;
                     break;
                  case InventorySlotID.INVTYPE_HANDS:
                     inventory[9] = item;
                     break;
                  case InventorySlotID.INVTYPE_FINGER:
                     inventory[10] = item;
                     break;
                  case InventorySlotID.INVTYPE_TRINKET:
                     inventory[12] = item;
                     break;
                  case InventorySlotID.INVTYPE_WEAPONMAINHAND:
                  case InventorySlotID.INVTYPE_WEAPON:
                  case InventorySlotID.INVTYPE_2HWEAPON:
                  case InventorySlotID.INVTYPE_RANGED:
                     inventory[15] = item;
                     break;
                  case InventorySlotID.INVTYPE_WEAPONOFFHAND:
                  case InventorySlotID.INVTYPE_SHIELD:
                  case InventorySlotID.INVTYPE_RANGEDRIGHT:
                     inventory[16] = item;
                     break;
               }
            }
         }
         return inventory;
      }
   }
}
