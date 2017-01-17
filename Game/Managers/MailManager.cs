using System;
using CobolWow.Tools;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Communication;
using CobolWow.Game.Handlers;
using CobolWow.Communication.Incoming.World.Mail;
using CobolWow.Communication.Outgoing.World.Mail;
using CobolWow.Database20.Tables;
using CobolWow.Game.Constants.Game.Mail;

namespace CobolWow.Game.Managers
{
   public class MailManager
   {
      public static void Boot()
      {
         WorldDataRouter.AddHandler<PCGetMailList>(WorldOpcodes.CMSG_GET_MAIL_LIST, OnGetMailList);
         WorldDataRouter.AddHandler<PCSendMail>(WorldOpcodes.CMSG_SEND_MAIL, OnSendMail);
         WorldDataRouter.AddHandler<PCSendMail>(WorldOpcodes.CMSG_MAIL_RETURN_TO_SENDER, OnReturnMailToSender);

         Logger.Log(LogType.Information, "MailManager Initialized.");
      }

      private static void OnReturnMailToSender(WorldSession session, PCSendMail handler)
      {
         throw new NotImplementedException();
      }

      private static void OnSendMail(WorldSession session, PCSendMail mail)
      {
         Character reciever = Database20.Characters.CharacterManager.GetCharacter(mail.Reciever);

         //var result = MailResponseResult.MAIL_OK;
         //if (reciever == null)
         //{
         //   result = MailResponseResult.MAIL_ERR_RECIPIENT_NOT_FOUND;
         //}
         //else if (reciever.name == session.Character.name)
         //{
         //   result = MailResponseResult.MAIL_ERR_CANNOT_SEND_TO_SELF;
         //}
         //else if (session.Character.money < mail.Money + 30)
         //{
         //   result = MailResponseResult.MAIL_ERR_NOT_ENOUGH_MONEY;
         //}
         //else if (Mails.Where(m => m.receiver == reciever.guid).ToArray().Length > 100)
         //{
         //   result = MailResponseResult.MAIL_ERR_RECIPIENT_CAP_REACHED;
         //}
         //else if (GetFaction(reciever) != GetFaction(session.Player.Character))
         //{
         //   result = MailResponseResult.MAIL_ERR_NOT_YOUR_TEAM;
         //}

         //if (packet.ItemGUID > 0)
         //{
         //   throw new NotImplementedException();
         //}

         //session.SendPacket(new PSSendMailResult(0, MailResponseType.MAIL_SEND, result));

         //if (result == MailResponseResult.MAIL_OK)
         //{
         //   session.Player.Character.money -= (int)(packet.Money + 30);
         //   Mails.Add(
         //       new mail()
         //       {
         //          messageType = (byte)MailMessageType.MAIL_NORMAL,
         //          deliver_time = 0,
         //          expire_time = (int)GameUnits.DAY * 30,
         //          @checked =
         //                   packet.Body != ""
         //                       ? (byte)MailCheckMask.MAIL_CHECK_MASK_HAS_BODY
         //                       : (byte)MailCheckMask.MAIL_CHECK_MASK_COPIED,
         //          cod = (int)packet.COD,
         //          has_items = 0,
         //          itemTextId = 0,
         //          money = (int)packet.Money,
         //          sender = session.Player.Character.guid,
         //          receiver = reciever.guid,
         //          subject = packet.Subject,
         //          stationery = (sbyte)MailStationery.MAIL_STATIONERY_DEFAULT,
         //          mailTemplateId = 0
         //       });
         //}
      }

      private static void OnGetMailList(WorldSession session, PCGetMailList handler)
      {
         //List<CharacterMail> Mails = DBMails.GetCharacterMails(session.Character);
         //session.SendPacket(new PSMailListResult(Mails));
      }
   }
}
