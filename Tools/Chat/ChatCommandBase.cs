using System;
using System.Reflection;

namespace CobolWow.Tools.Chat
{
   public class ChatCommandBase : Attribute
   {
      public string Name { get; set; }
      public string Description { get; set; }

      public MethodInfo Method { get; set; }

      public ChatCommandBase(string name, string description = "")
      {
         Name = name;
         Description = description;
      }
   }
}
