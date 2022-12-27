using Labloader.Core.API.Enums;
using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class Scp914UpgradingPlayerEventArgs : System.EventArgs
    {
        public Player Player { get; }
        
        public Scp914Dial Dial { get; }

        public bool IsAllowed { get; set; } = true;

        public Scp914UpgradingPlayerEventArgs(Player player, Scp914Dial dial)
        {
            Player = player;
            Dial = dial;
        }
    }
}