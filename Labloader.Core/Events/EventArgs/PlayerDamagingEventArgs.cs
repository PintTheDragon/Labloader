using JetBrains.Annotations;
using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerDamagingEventArgs : System.EventArgs
    {
        [CanBeNull]
        public Player Attacker { get; }
        
        public Player Target { get; }
        
        public float Damage { get; set; }

        public bool IsAllowed { get; set; } = true;
        
        internal PlayerDamagingEventArgs(Player attacker, Player target, float damage)
        {
            Attacker = attacker;
            Target = target;
            Damage = damage;
        }
    }
}