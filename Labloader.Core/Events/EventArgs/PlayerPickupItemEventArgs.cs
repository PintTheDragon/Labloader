using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerPickupItemEventArgs
    {
        public Player Player { get; }
        
        public Item Item { get; }

        public PlayerPickupItemEventArgs(Player player, Item item)
        {
            Player = player;
            Item = item;
        }
    }
}