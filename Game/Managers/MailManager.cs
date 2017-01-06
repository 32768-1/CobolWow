﻿using System;
using CobolWow.Tools;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Communication;
using CobolWow.Game.Handlers;
using CobolWow.Game.Constants.Game;
using CobolWow.Tools.Database.Tables;
using CobolWow.Tools.Database.Helpers;
using CobolWow.Game.Constants.Game.Mail;
using CobolWow.Communication.Incoming.World.Mail;
using CobolWow.Communication.Outgoing.World.Mail;

namespace CobolWow.Game.Managers
{
   public class MailManager
   {
      private static Log Logger = new Log();

      public static void Boot()
      {
         WorldDataRouter.AddHandler<PCGetMailList>(WorldOpcodes.CMSG_GET_MAIL_LIST, OnGetMailList);
         WorldDataRouter.AddHandler<PCSendMail>(WorldOpcodes.CMSG_SEND_MAIL, OnSendMail);
         WorldDataRouter.AddHandler<PCSendMail>(WorldOpcodes.CMSG_MAIL_RETURN_TO_SENDER, OnReturnMailToSender);

         Logger.Print(LogType.Information, "MailManager Initialized.");
      }

      private static void OnReturnMailToSender(WorldSession session, PCSendMail handler)
      {
         throw new NotImplementedException();
      }

      private static void OnSendMail(WorldSession session, PCSendMail mail)
      {
         Character reciever = DBCharacters.GetCharacter(mail.Reciever);
         MailResponseResult result = MailResponseResult.MAIL_OK;
         if (reciever == null) result = MailResponseResult.MAIL_ERR_RECIPIENT_NOT_FOUND;
         else if (reciever.Name == session.Character.Name) result = MailResponseResult.MAIL_ERR_CANNOT_SEND_TO_SELF;
         else if (session.Character.Money < mail.Money + 30) result = MailResponseResult.MAIL_ERR_NOT_ENOUGH_MONEY;
         else if (DBMails.GetCharacterMails(reciever).Count > 100) result = MailResponseResult.MAIL_ERR_RECIPIENT_CAP_REACHED;
         else if (reciever.Faction != session.Character.Faction) result = MailResponseResult.MAIL_ERR_NOT_YOUR_TEAM;

         if (mail.ItemGUID > 0)
         {
            throw new NotImplementedException();
         }

         session.SendPacket(new PSSendMailResult(0, MailResponseType.MAIL_SEND, result));

         if (result == MailResponseResult.MAIL_OK)
         {
            session.Character.Money -= (int)(mail.Money + 30);

            DBMails.AddMail(new CharacterMail()
            {
               MessageType = MailMessageType.MAIL_NORMAL,
               Deliver_Time = 0,
               Expire_Time = (int)GameUnits.DAY * 30,
               Checked = mail.Body != "" ? MailCheckMask.MAIL_CHECK_MASK_HAS_BODY : MailCheckMask.MAIL_CHECK_MASK_COPIED,
               COD = (int)mail.COD,
               Has_Items = 0,
               ItemTextID = 0,
               Money = (int)mail.Money,
               Sender = session.Character.GUID,
               Reciever = reciever.GUID,
               Subject = mail.Subject,
               Stationery = MailStationery.MAIL_STATIONERY_DEFAULT,
               MailTemplateID = 0,
               Body = mail.Body
            });
         }
      }

      private static void OnGetMailList(WorldSession session, PCGetMailList handler)
      {
         List<CharacterMail> Mails = DBMails.GetCharacterMails(session.Character);
         session.SendPacket(new PSMailListResult(Mails));
      }
   }
}