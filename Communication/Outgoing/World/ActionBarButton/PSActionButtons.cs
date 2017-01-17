using System.Collections.Generic;
using CobolWow.Network.Packets;
using CobolWow.Database20.Tables;

namespace CobolWow.Communication.Outgoing.World.ActionBarButton
{
   class PSActionButtons : ServerPacket
   {
      public PSActionButtons(Character character) : base(WorldOpcodes.SMSG_ACTION_BUTTONS)
      {
         List<CharacterActionBarButton> savedButtons = Database20.Characters.ActionButtonsManager.GetActionBarButtons(character);

         for (int button = 0; button < 120; button++)
         {
            int index = savedButtons.FindIndex(b => b.button == button);

            CharacterActionBarButton currentButton = index != -1 ? savedButtons[index] : null;

            if (currentButton != null)
            {
               uint packedData = (uint)currentButton.action | (uint)currentButton.type << 24;

               Write(packedData);
            }
            else
            {
               Write((uint)0);
            }
         }
      }
   }
}
