using Dapper.Contrib.Extensions;

namespace CobolWow.Database20.Tables
{
   [Table("mail")]
   public class ChrMail
   {
      public long id { get; set; }
      public byte messageType { get; set; }
      public sbyte stationery { get; set; }
      public int mailTemplateId { get; set; }
      public long sender { get; set; }
      public long receiver { get; set; }
      public string subject { get; set; }
      public long itemTextId { get; set; }
      public byte has_items { get; set; }
      public decimal expire_time { get; set; }
      public decimal deliver_time { get; set; }
      public long money { get; set; }
      public long cod { get; set; }
      public byte @checked { get; set; }
   }
}
