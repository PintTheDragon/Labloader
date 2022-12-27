using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerDroppingItemEventArgs : System.EventArgs
    {
        public Player Player { get; }
        
        public Item Item { get; }
        
        public bool IsAllowed { get; set; } = true;

        public PlayerDroppingItemEventArgs(Player player, Item item)
        {
            Player = player;
            Item = item;
        }
    }
}