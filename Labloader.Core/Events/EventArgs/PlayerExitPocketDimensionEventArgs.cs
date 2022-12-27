using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerExitPocketDimensionEventArgs : System.EventArgs
    {
        public Player Player { get; }

        public PlayerExitPocketDimensionEventArgs(Player player)
        {
            Player = player;
        }
    }
}