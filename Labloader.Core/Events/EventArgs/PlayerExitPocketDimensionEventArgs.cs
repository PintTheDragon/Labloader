using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerExitPocketDimensionEventArgs
    {
        public Player Player { get; }

        public PlayerExitPocketDimensionEventArgs(Player player)
        {
            Player = player;
        }
    }
}