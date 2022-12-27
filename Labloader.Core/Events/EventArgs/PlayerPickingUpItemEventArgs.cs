using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerPickingUpItemEventArgs : System.EventArgs
    {
        public Player Player { get; }
        
        public Item Item { get; }
        
        public bool IsAllowed { get; set; } = true;

        public PlayerPickingUpItemEventArgs(Player player, Item item)
        {
            Player = player;
            Item = item;
        }
    }
}