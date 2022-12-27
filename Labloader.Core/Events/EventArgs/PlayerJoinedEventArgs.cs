using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public struct PlayerJoinedEventArgs
    {
        public Player Player { get; }
        
        internal PlayerJoinedEventArgs(Player player)
        {
            Player = player;
        }
    }
}