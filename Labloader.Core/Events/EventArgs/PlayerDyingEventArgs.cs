using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerDyingEventArgs : System.EventArgs
    {
        public Player Attacker { get; }
        
        public Player Target { get; }
        
        public bool IsAllowed { get; set; } = true;
        
        internal PlayerDyingEventArgs(Player attacker, Player target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}