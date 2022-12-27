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
        
        internal static event EventHandler<PlayerDyingEventArgs> PlayerDying;
        internal static void OnPlayerDying(PlayerDyingEventArgs ev) => PlayerDying?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerBannedEventArgs> PlayerBanned;
        internal static void OnPlayerBanned(PlayerBannedEventArgs ev) => PlayerBanned?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerExecutingCommandEventArgs> PlayerExecutingCommand;
        internal static void OnPlayerExecutingCommand(PlayerExecutingCommandEventArgs ev) => PlayerExecutingCommand?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerPickingUpItemEventArgs> PlayerPickingUpItem;
        internal static void OnPlayerPickingUpItem(PlayerPickingUpItemEventArgs ev) => PlayerPickingUpItem?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerDroppingItemEventArgs> PlayerDroppingItem;
        internal static void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev) => PlayerDroppingItem?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerShootingGunEventArgs> PlayerShootingGun;
        internal static void OnPlayerShootIngGun(PlayerShootingGunEventArgs ev) => PlayerShootingGun?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerDamagingEventArgs> PlayerDamaging;
        internal static void OnPlayerDamaging(PlayerDamagingEventArgs ev) => PlayerDamaging?.Invoke(null, ev);
        
        internal static event EventHandler<PlayerReloadingGunEventArgs> PlayerReloadingGun;
        internal static void OnPlayerReloadingGun(PlayerReloadingGunEventArgs ev) => PlayerReloadingGun?.Invoke(null, ev);
        
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