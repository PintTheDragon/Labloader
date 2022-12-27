using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerDiedEventArgs
    {
        public Player Attacker { get; }
        
        public Player Target { get; }
        
        internal PlayerDiedEventArgs(Player attacker, Player target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}