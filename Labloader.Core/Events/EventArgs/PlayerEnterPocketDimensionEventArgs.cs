using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerEnterPocketDimensionEventArgs
    {
        public Player Player { get; }

        public PlayerEnterPocketDimensionEventArgs(Player player)
        {
            Player = player;
        }
    }
}