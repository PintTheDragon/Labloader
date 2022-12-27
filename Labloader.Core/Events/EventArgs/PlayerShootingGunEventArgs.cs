using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerShootingGunEventArgs
    {
        public Player Attacker { get; }
        
        public Player Target { get; }
        
        public Item Gun { get; }
        
        public bool IsAllowed { get; set; } = true;

        public PlayerShootingGunEventArgs(Player attacker, Player target, Item gun)
        {
            Attacker = attacker;
            Target = target;
            Gun = gun;
        }
    }
}