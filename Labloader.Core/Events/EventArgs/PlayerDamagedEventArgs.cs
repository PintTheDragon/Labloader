using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerDamagedEventArgs
    {
        public Player Attacker { get; }
        
        public Player Target { get; }
        
        public float Damage { get; set; }
        
        internal PlayerDamagedEventArgs(Player attacker, Player target, float damage)
        {
            Attacker = attacker;
            Target = target;
            Damage = damage;
        }
    }
}