using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerBannedEventArgs : System.EventArgs
    {
        // TODO: Issuer.
        public Player Player;

        public PlayerBannedEventArgs(Player player)
        {
            Player = player;
        }
    }
}