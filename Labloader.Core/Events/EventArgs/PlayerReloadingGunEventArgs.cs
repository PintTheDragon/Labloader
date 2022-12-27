using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerReloadingGunEventArgs
    {
        public Player Player { get; }
        
        public Item Gun { get; }
        
        public bool IsAllowed { get; set; } = true;

        public PlayerReloadingGunEventArgs(Player player, Item gun)
        {
            Player = player;
            Gun = gun;
        }
    }
}