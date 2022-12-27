using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerDropItemEventArgs
    {
        public Player Player { get; }
        
        public Item Item { get; }

        public PlayerDropItemEventArgs(Player player, Item item)
        {
            Player = player;
            Item = item;
        }
    }
}