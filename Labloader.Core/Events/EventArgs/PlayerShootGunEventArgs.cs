using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerShootGunEventArgs
    {
        public Player Attacker { get; }
        
        public Player Target { get; }
        
        public Item Gun { get; }

        public PlayerShootGunEventArgs(Player attacker, Player target, Item gun)
        {
            Attacker = attacker;
            Target = target;
            Gun = gun;
        }
    }
}