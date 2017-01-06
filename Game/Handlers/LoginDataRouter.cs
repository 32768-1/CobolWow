using System;
using System.Collections.Generic;

using CobolWow.Tools;
using CobolWow.Game.Sessions;
using CobolWow.Communication;

namespace CobolWow.Game.Handlers
{
   public delegate void ProcessLoginPacketCallback(LoginSession Session, byte[] data);
   public delegate void ProcessLoginPacketCallbackTypes<T>(LoginSession Session, T handler);

   public class LoginDataRouter
   {
      private static Dictionary<LoginOpcodes, ProcessLoginPacketCallback> mCallbacks = new Dictionary<LoginOpcodes, ProcessLoginPacketCallback>();
      private static Log Logger = new Log();
      public static void AddHandler(LoginOpcodes opcode, ProcessLoginPacketCallback handler)
      {
         mCallbacks.Add(opcode, handler);
      }

      public static void AddHandler<T>(LoginOpcodes opcode, ProcessLoginPacketCallbackTypes<T> callback)
      {
         AddHandler(opcode, (session, data) =>
         {
            T generatedHandler = (T)Activator.CreateInstance(typeof(T), data);
            callback(session, generatedHandler);
         });
      }

      public static void CallHandler(LoginSession session, LoginOpcodes opcode, byte[] data)
      {
         if (mCallbacks.ContainsKey(opcode))
         {
            mCallbacks[opcode](session, data);
         }
         else
         {
            Logger.Print(LogType.Warning, "Missing handler: " + opcode);
         }
      }
   }
}
