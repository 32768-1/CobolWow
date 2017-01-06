using System;
using CobolWow.Net;
using CobolWow.Game.Entitys;
using CobolWow.Game.Constants.Game.Update;
using CobolWow.Game.Constants.Game.World.Entity;

namespace CobolWow.Tools.Chat.Commands
{
   [ChatCommandNode("modify", "Modify commands")]
   public class Modify
   {
      public static void Default(WorldSession session, string[] args)
      {
         if (args.Length == 1 && args[0].ToLower() == "list")
         {
            session.SendMessage("List");
         }
         else if (args.Length == 2)
         {
            string attributeName = args[0].ToLower();
            string attributeValue = args[1];

            // If player isn't targeting. Target self
            UnitEntity entity = session.Entity.Target ?? session.Entity;

            bool unknownAttribute = false;

            switch (attributeName)
            {
               case "scale":
                  entity.Scale = float.Parse(attributeValue);
                  break;

               case "health":
                  entity.Health = int.Parse(attributeValue);
                  break;

               case "level":
                  entity.Level = int.Parse(attributeValue);
                  break;

               case "xp":
                  (entity as PlayerEntity).XP = int.Parse(attributeValue);
                  break;

               case "gender":
                  entity.SetUpdateField<byte>((int)EUnitFields.UNIT_FIELD_BYTES_0, (byte)int.Parse(attributeValue), 2);
                  break;

               case "model":
                  entity.SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_DISPLAYID, int.Parse(attributeValue));
                  break;

               case "state":
                  entity.SetUpdateField<byte>((int)EUnitFields.UNIT_NPC_EMOTESTATE, (byte)int.Parse(attributeValue));
                  break;
               case "money":
                  int moneyToAdd = int.Parse(attributeValue) < 0x7fffffff ? int.Parse(attributeValue) : 0x7fffffff;
                  entity.SetUpdateField<Int32>((int)EUnitFields.PLAYER_FIELD_COINAGE, moneyToAdd);
                  session.Character.Money = moneyToAdd;
                  break;
               case "standstate":
                  entity.SetStandState((UnitStandStateType)int.Parse(attributeValue));
                  break;
               default:
                  unknownAttribute = true;
                  break;
            }

            if (unknownAttribute)
            {
               session.SendMessage("Attribute '" + attributeName + "' was unknown");
            }
            else
            {
               session.SendMessage("Applied " + attributeName + " = " + attributeValue + "");
            }
         }
      }
   }

}
