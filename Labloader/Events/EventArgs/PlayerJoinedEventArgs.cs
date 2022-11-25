using Labloader.API.Features;

namespace Labloader.Events.EventArgs
{
    public struct PlayerJoinedEventArgs
    {
        public Player Player { get; set; }
        
        internal PlayerJoinedEventArgs(Player player)
        {
            Player = player;
        }
    }
}