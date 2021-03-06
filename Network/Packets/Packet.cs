﻿using System;
using System.IO;
using System.Collections.Generic;

using CobolWow.Game.Constants;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace CobolWow.Network.Packets
{
   public class Packet
   {
      /// <summary>
      /// Default ctor, just for use to create our stuffs.
      /// </summary>
      private Packet()
      { }

      public Packet(byte[] data)
      {
         Reader = new PacketReader(data);
      }

      public Packet(PacketHeaderType header, byte opCode)
      {
         HeaderType = header;
         Writer = new PacketWriter(header);
         // Keep in mind; depending on the type of packet header passed,
         // this will write either a byte, or a ushort.
         Writer.WritePacketHeader(opCode);
      }

      #region Compress/Decompress

      private Deflater Deflater = new Deflater();
      public byte[] GetCompressedOutPacket(int offset, int length)
      {
         Deflater.Reset();
         Deflater.SetInput(Data, offset, length);
         Deflater.Finish();

         var compBuffer = new byte[1024];
         var ret = new List<byte>();

         while (!Deflater.IsFinished)
         {
            try
            {
               Deflater.Deflate(compBuffer);
               ret.AddRange(compBuffer);
               Array.Clear(compBuffer, 0, compBuffer.Length);
            }
            catch (Exception ex)
            {
               throw ex;
            }
         }
         Deflater.Reset();
         return ret.ToArray();
      }

      public byte[] GetCompressedOutPacket()
      {
         return GetCompressedOutPacket(0, Data.Length);
      }

      #endregion

      public ushort OpCode { get; private set; }

      public byte[] Data
      {
         get
         {
            // How do you like THAT!?
            return Writer != null
                       ? (Writer.BaseStream as MemoryStream).ToArray()
                       : (Reader != null ? (Reader.BaseStream as MemoryStream).ToArray() : null);
         }
      }

      public PacketWriter Writer { get; private set; }
      public PacketReader Reader { get; private set; }

      protected PacketHeaderType HeaderType { get; set; }
   }
}
