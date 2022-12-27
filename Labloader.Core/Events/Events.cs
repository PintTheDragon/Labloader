using System;
using System.Reflection;
using Labloader.Core.Events.EventArgs;

namespace Labloader.Core.Events
{
    internal static class Events
    {
        internal static EventInfo GetEvent(EventType eventType)
        {
            return typeof(Events).GetEvent(eventType.ToString());
        }

        internal static event EventHandler<PlayerJoinedEventArgs> PlayerJoined;
        internal static void OnPlayerJoined(PlayerJoinedEventArgs ev) => PlayerJoined?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerLeftEventArgs> PlayerLeft;
        internal static void OnPlayerLeft(PlayerLeftEventArgs ev) => PlayerLeft?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerDiedEventArgs> PlayerDied;
        internal static void OnPlayerDied(PlayerDiedEventArgs ev) => PlayerDied?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerBannedEventArgs> PlayerBanned;
        internal static void OnPlayerBanned(PlayerBannedEventArgs ev) => PlayerBanned?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerExecutingCommandEventArgs> PlayerExecutingCommand;
        internal static void OnPlayerExecutingCommand(PlayerExecutingCommandEventArgs ev) => PlayerExecutingCommand?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerPickupItemEventArgs> PlayerPickupItem;
        internal static void OnPlayerPickupItem(PlayerPickupItemEventArgs ev) => PlayerPickupItem?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerDropItemEventArgs> PlayerDropItem;
        internal static void OnPlayerDropItem(PlayerDropItemEventArgs ev) => PlayerDropItem?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerShootGunEventArgs> PlayerShootGun;
        internal static void OnPlayerShootGun(PlayerShootGunEventArgs ev) => PlayerShootGun?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerDamagedEventArgs> PlayerDamaged;
        internal static void OnPlayerDamaged(PlayerDamagedEventArgs ev) => PlayerDamaged?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerReloadGunEventArgs> PlayerReloadGun;
        internal static void OnPlayerReloadGun(PlayerReloadGunEventArgs ev) => PlayerReloadGun?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerEnterPocketDimensionEventArgs> PlayerEnterPocketDimension;
        internal static void OnPlayerEnterPocketDimension(PlayerEnterPocketDimensionEventArgs ev) => PlayerEnterPocketDimension?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerExitPocketDimensionEventArgs> PlayerExitPocketDimension;
        internal static void OnPlayerExitPocketDimension(PlayerExitPocketDimensionEventArgs ev) => PlayerExitPocketDimension?.Invoke(null, ev);
        
        internal static event EventHandler<Scp914UpgradingItemEventArgs> Scp914UpgradingItem;
        internal static void OnScp914UpgradingItem(Scp914UpgradingItemEventArgs ev) => Scp914UpgradingItem?.Invoke(null, ev);
        
        internal static event EventHandler<Scp914UpgradingPlayerEventArgs> Scp914UpgradingPlayer;
        internal static void OnScp914UpgradingPlayer(Scp914UpgradingPlayerEventArgs ev) => Scp914UpgradingPlayer?.Invoke(null, ev);
    }
}