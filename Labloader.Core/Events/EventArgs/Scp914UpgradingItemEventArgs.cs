using Labloader.Core.API.Enums;
using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class Scp914UpgradingItemEventArgs
    {
        public Item Item { get; }
        
        public ItemType NewType { get; set; }
        
        public Scp914Dial Dial { get; }

        public bool IsAllowed { get; set; } = true;

        public Scp914UpgradingItemEventArgs(Item item, ItemType newType, Scp914Dial dial)
        {
            Item = item;
            NewType = newType;
            Dial = dial;
        }
    }
}