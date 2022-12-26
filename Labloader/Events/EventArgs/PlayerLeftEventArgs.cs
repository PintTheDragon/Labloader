using Labloader.API.Features;

namespace Labloader.Events.EventArgs
{
    public class PlayerLeftEventArgs
    {
        public Player Player { get; }
        
        internal PlayerLeftEventArgs(Player player)
        {
            Player = player;
        }
    }
}