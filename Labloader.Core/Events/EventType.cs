namespace Labloader.Core.Events
{
    public enum EventType
    {
        PlayerJoined = 0,
        PlayerLeft = 1,
        // TODO: Implement these.
        PlayerDied = 2,
        PlayerBanned = 3,
        PlayerExecutingCommand = 4,
        PlayerPickupItem = 5,
        PlayerDropItem = 6,
        PlayerShootGun = 7,
        PlayerDamaged = 8,
        PlayerReloadGun = 9,
        PlayerEnterPocketDimension = 10,
        PlayerExitPocketDimension = 11,
        Scp914UpgradeItem = 12,
        Scp914UpgradePlayer = 13,
    }
}