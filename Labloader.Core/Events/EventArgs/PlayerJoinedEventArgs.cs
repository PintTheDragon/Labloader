using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerJoinedEventArgs : System.EventArgs
    {
        public Player Player { get; }
        
        internal PlayerJoinedEventArgs(Player player)
        {
            Player = player;
        }
    }
}