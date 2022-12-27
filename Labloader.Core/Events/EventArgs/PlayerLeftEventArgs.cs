using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
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