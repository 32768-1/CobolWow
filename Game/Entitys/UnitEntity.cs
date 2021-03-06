﻿using System;
using System.Threading;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Network;
using CobolWow.Communication;
using CobolWow.Game.Managers;
using CobolWow.Network.Packets;
using CobolWow.Tools.Extensions;
using CobolWow.Tools.Database.Tables;
using CobolWow.Game.Constants.Game.Update;
using CobolWow.Game.Constants.Game.World.Entity;
using CobolWow.Database20.Tables;

namespace CobolWow.Game.Entitys
{
   public class AIBrainManager
   {
      public static List<AIBrain> AIBrains = new List<AIBrain>();

      public static void Boot()
      {
         new Thread(UpdateThread).Start();
      }

      private static void UpdateThread()
      {
         while (true)
         {
            Update();
            Thread.Sleep(1000);
         }
      }

      private static void Update()
      {
         AIBrains.ForEach(ai => ai.Update());
      }
   }

   public class AIBrain
   {
      public PlayerEntity Target;
      public UnitEntity Entity;

      public AIBrain(UnitEntity entity)
      {
         Entity = entity;

         AIBrainManager.AIBrains.Add(this);
      }

      public void Update()
      {
         if (Target == null)
         {
            WorldSession session = WorldServer.Sessions.Find(s => global::CobolWow.Tools.Helper.Distance(s.Entity.X, s.Entity.Y, Entity.X, Entity.Y) < 30);

            if (session != null)
            {
               Target = session.Entity;
            }
         }
         else
         {
            Entity.Move(Target.X, Target.Y, Target.Z);
         }
      }
   }

   public class UnitEntity : ObjectEntity, ILocation
   {
      public float X, Y, Z, R;

      public override int DataLength
      {
         get { return (int)EUnitFields.UNIT_END - 0x4; }
      }

      public override string Name
      {
         get { return Template.Name; }
      }


      public int Health
      {
         get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_HEALTH]; }
         set { SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_HEALTH, value); }
      }

      public int MaxHealth
      {
         get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_MAXHEALTH]; }
         set { SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_MAXHEALTH, value); }
      }

      public int Level
      {
         get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_LEVEL]; }
         set { SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_LEVEL, value); }
      }

      public int EmoteState
      {
         get { return (int)UpdateData[(int)EUnitFields.UNIT_NPC_EMOTESTATE]; }
         set { SetUpdateField<int>((int)EUnitFields.UNIT_NPC_EMOTESTATE, value); }
      }

      public int DisplayID
      {
         get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_DISPLAYID]; }
         set { SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_DISPLAYID, value); }
      }

      public CreatureEntry TEntry;
      public CreatureTemplate Template;

      public UnitEntity(ObjectGUID objectGUID)
          : base(objectGUID)
      {
      }

      public UnitEntity(CreatureEntry entry = null, ObjectGUID guid = null) : base((guid == null) ? ObjectGUID.GetUnitGUID((uint)entry.guid) : guid)
      {
         //new AIBrain(this);

         //TEntry = entry;

         //CreatureTemplate template = DBC.CreatureTemplates.Find(a => a.entry == entry.id);

         //Template = template;

         //Type = (byte)0x9;
         //Entry = (byte)template.Entry;
         ////Data = -248512512;

         //X = entry.position_x;
         //Y = entry.position_y;
         //Z = entry.position_z;
         //R = entry.orientation;

         //SetUpdateField<int>((int)EUnitFields.UNIT_NPC_FLAGS, template.npcflag);
         //SetUpdateField<int>((int)EUnitFields.UNIT_DYNAMIC_FLAGS, template.dynamicflags);
         //SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_FLAGS, template.unit_flags);

         //SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_FACTIONTEMPLATE, template.faction_A);

         //SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_HEALTH, entry.curhealth);
         //SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_MAXHEALTH, template.maxhealth);
         //SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_LEVEL, template.maxlevel);
         //DisplayID = (entry.modelid != 0) ? entry.modelid : TEntry.modelid;

         //SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_CREATEDBY, 0);
      }

      public void SetStandState(UnitStandStateType state)
      {
         SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_BYTES_1, (byte)state, 0);

         if (this is PlayerEntity)
         {
            using (ServerPacket packet = new ServerPacket(WorldOpcodes.SMSG_STANDSTATE_UPDATE))
            {
               packet.Write((byte)state);
               (this as PlayerEntity).Session.SendPacket(packet);
            }
         }
      }

      /*        public void PlayEmote(Emote emoteID)
              {
                  if (emoteID == 0) EmoteState = (int)emoteID;
                  else
                  {
                      EmotesEntry emote = DBC.Emotes.List.Find(e => e.ID == (int)emoteID);
                      if (emote.EmoteType == 0) EmoteState = (int)emoteID;
                      else
                      {
                          WorldSession session = (this as PlayerEntity).Session;
                          if(session != null) session.sendPacket(new PSEmote((uint)emoteID, session.Character.GUID));
                      }
                  }
              }*/

      public void Move(float target_position_x, float target_position_y, float target_position_z)
      {
         using (ServerPacket packet = new ServerPacket(WorldOpcodes.SMSG_MONSTER_MOVE))
         {
            packet.WritePackedUInt64(ObjectGUID.RawGUID);
            packet.Write(X);
            packet.Write(Y);
            packet.Write(Z);
            packet.Write((UInt32)Environment.TickCount);
            packet.Write((byte)0); // SPLINETYPE_NORMAL
            packet.Write(0); // sPLINE FLAG
            packet.Write(500); // TIME
            packet.Write(1);
            packet.Write(target_position_x);
            packet.Write(target_position_y);
            packet.Write(target_position_z);

            World.PlayersWhoKnowUnit(this).ForEach(e => e.Session.SendPacket(packet));

            X = target_position_x;
            Y = target_position_y;
            Z = target_position_z;
         }
      }
   }
}