using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerReloadGunEventArgs
    {
        public Player Player { get; }
        
        public Item Gun { get; }

        public PlayerReloadGunEventArgs(Player player, Item gun)
        {
            Player = player;
            Gun = gun;
        }
    }
}