namespace Labloader.Core.Events
{
    public enum EventType
    {
        PlayerJoined = 0,
        PlayerLeft = 1,
        // TODO: Implement these.
        PlayerDying = 2,
        PlayerBanned = 3,
        PlayerExecutingCommand = 4,
        PlayerPickingUpItem = 5,
        PlayerDroppingItem = 6,
        PlayerShootingGun = 7,
        PlayerDamaging = 8,
        PlayerReloadingGun = 9,
        PlayerEnterPocketDimension = 10,
        PlayerExitPocketDimension = 11,
        Scp914UpgradingItem = 12,
        Scp914UpgradingPlayer = 13,
    }
}