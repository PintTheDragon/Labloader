using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using Labloader.Core.API.Enums;
using RiptideNetworking.Transports.RudpTransport;
using UnityEngine;

namespace Labloader.Core.API.Features
{
    public struct Player
    {
        #region STATIC

        private static List<Player> _list = new List<Player>();

        internal static void UpdateList()
        {
            _list = NetworkManager.instance.connectedPlayers.Select(x => new Player(x.Key)).ToList();
        }

        /// <summary>
        /// Gets all of the players in the server.
        /// </summary>
        public static ReadOnlyCollection<Player> List => _list.AsReadOnly();

        /// <summary>
        /// Maps unity object name (PlayerCharacter.name) to role enum.
        /// </summary>
        private static readonly Dictionary<string, Role> RoleMap = new Dictionary<string, Role>()
        {
            {"D-9341", Role.ClassD},
            {"D-9634", Role.ClassD},
            {"D-9758", Role.ClassD},
            {"D-9843", Role.ClassD},
            {"MTF", Role.Mtf},
            {"MTF 2", Role.Mtf},
            {"MTF 3", Role.Mtf},
            {"MTF 4", Role.Mtf},
            {"O5", Role.O5},
            {"Scientist", Role.Scientist},
            {"SCP-173", Role.Scp173},
        };

        /// <summary>
        /// Maps unity object name (PlayerCharacter.name) to role enum.
        /// </summary>
        private static readonly Dictionary<string, Team> TeamMap = new Dictionary<string, Team>()
        {
            { "CBTeam", Team.ContainmentBreach },
            { "ClassDTeam", Team.ClassD },
            { "MTFTeam", Team.Mtf },
            { "SCPTeam", Team.Scp },
        };

        /// <summary>
        /// Gets the specified player by their GameObject.
        /// </summary>
        /// <param name="obj">The player's GameObject.</param>
        /// <returns>The Player.</returns>
        public static Player Get(GameObject obj) => Get(obj.GetComponent<NetworkObject>().PlayerId.Value);

        /// <summary>
        /// Gets the specified player by their ID (specific to this server/session).
        /// </summary>
        /// <param name="id">The player's ID.</param>
        /// <returns>The Player.</returns>
        public static Player Get(ushort id) => new Player(id);
        #endregion

        #region COMPONENT

        private ushort id;

        private Player(ushort id)
        {
            this.id = id;
        }
        #endregion

        #region APIProps

        /// <summary>
        /// Gets if this Player reference is valid.
        /// </summary>
        public bool IsValid => this.id != 0 && NetworkManager.instance.connectedPlayers.ContainsKey(this.id);

        /// <summary>
        /// Gets the player's NetworkPlayer.
        /// </summary>
        public NetworkPlayer NetworkPlayer => NetworkManager.instance.connectedPlayers[this.id].player;

        /// <summary>
        /// Gets the player's NetworkPlayerObject.
        /// </summary>
        public NetworkPlayerObject NetworkPlayerObject => NetworkManager.instance.connectedPlayers[this.id].playerObject;

        /// <summary>
        /// Gets the player's GameObject.
        /// </summary>
        public GameObject GameObject => this.NetworkPlayerObject == null ? this.NetworkPlayer.gameObject : this.NetworkPlayerObject.gameObject;

        /// <summary>
        /// The user's client ID (unique and specific to this server/session).
        /// </summary>
        public ushort ID => id;

        /// <summary>
        /// Gets the player's user ID (their ID through steam/discord/bbg), or null if they don't have one.
        /// </summary>
        [CanBeNull]
        public string UserID
        {
            get
            {
                var id = NetworkManager.instance.connectedPlayers[this.ID].authUserId;
                return string.IsNullOrEmpty(id) ? null : id;
            }
        }

        /// <summary>
        /// Gets the player's dimension.
        /// </summary>
        public RoomIdentity.ZoneType Dimension => this.NetworkPlayerObject != null ? this.NetworkPlayerObject.GetZone() : RoomIdentity.ZoneType.Unknown;

        /// <summary>
        /// Gets the player's name.
        /// </summary>
        public string Name => this.NetworkPlayerObject != null ? this.NetworkPlayerObject.PlayerName : this.NetworkPlayer.PlayerName;

        /// <summary>
        /// Gets the player's health.
        /// </summary>
        public float Health => this.NetworkPlayerObject != null ? this.NetworkPlayerObject.stats.health.health : 0;

        /// <summary>
        /// Damages the player.
        /// </summary>
        /// <param name="damage">The amount of damage to deal.</param>
        public void Damage(float damage)
        {
            if (this.NetworkPlayerObject == null) return;
            // TODO: Message and killer.
            this.NetworkPlayerObject.stats.health.Damage(damage);
        }

        /// <summary>
        /// Gets the player's current role.
        /// </summary>
        public Role Role =>
            (this.NetworkPlayerObject == null || this.NetworkPlayerObject.stats.health.dead)
                ? Role.Spectator
                : (RoleMap.TryGetValue(this.NetworkPlayerObject.CurrentCharacter.name, out var role) ? role : Role.None);

        /// <summary>
        /// Gets the player's current team.
        /// </summary>
        public Team Team =>
            (this.NetworkPlayerObject == null || this.NetworkPlayerObject.stats.health.dead)
                ? Team.Dead
                : (TeamMap.TryGetValue(this.NetworkPlayerObject.CurrentTeam.name, out var team) ? team : Team.None);

        /// <summary>
        /// Gets the player's IP address, or null if none exists.
        /// </summary>
        public string IP => !((RudpServer)NetworkManager.instance.server.server).TryGetClient(this.ID, out var client) ? null : client.RemoteEndPoint.Address.ToString();

        /// <summary>
        /// Gets the player's transform.
        /// </summary>
        public Transform Transform => this.GameObject.transform;

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        public Vector3 Position
        {
            get => this.Transform.position;
            set => this.Transform.position = value;
        }

        /// <summary>
        /// Gets or sets the player's rotation.
        /// </summary>
        public Quaternion Rotation
        {
            get => this.Transform.rotation;
            set => this.Transform.rotation = value;
        }

        /// <summary>
        /// Gets or sets the player's scale.
        /// </summary>
        public Vector3 Scale
        {
            get => this.Transform.localScale;
            set => this.Transform.localScale = value;
        }

        /// <summary>
        /// Gets the player's walk speed.
        /// </summary>
        public float WalkSpeed => this.NetworkPlayerObject != null ? this.NetworkPlayerObject.stats.data.walkSpeed : 0;

        /// <summary>
        /// Gets the player's sprint speed.
        /// </summary>
        public float SprintSpeed => this.NetworkPlayerObject != null ? this.NetworkPlayerObject.stats.data.sprintSpeed : 0;

        /// <summary>
        /// Gets the player's crouch speed.
        /// </summary>
        public float CrouchSpeed => this.NetworkPlayerObject != null ? this.NetworkPlayerObject.stats.data.crouchSpeed : 0;

        /// <summary>
        /// Gets or sets whether or not the player is noclipping.
        /// </summary>
        public bool Noclip
        {
            get => this.NetworkPlayerObject != null ? this.NetworkPlayerObject.NoclipController.enabled : false;
            set
            {
                if (this.NetworkPlayerObject == null) return;

                this.NetworkPlayerObject.NoclipController.enabled = value;
            }
        }
        #endregion

        #region APIMethods
        /// <summary>
        /// Kills the player.
        /// </summary>
        public void Kill(string message = null)
        {
            if (this.NetworkPlayerObject == null) return;

            // TODO: Message, Killer.
            this.NetworkPlayerObject.stats.health.Kill();
        }

        /// <summary>
        /// Kick a player from the server.
        /// </summary>
        /// <param name="message">The message to be sent to the player.</param>
        public void Kick(string message = "You have been kicked from the server!")
        {
            // TODO: Kick messages.
            NetworkManager.instance.Kick(this.NetworkPlayer.netObj.ClientId, message);
        }

        /// <summary>
        /// Permanently bans the player.
        /// </summary>
        /// <param name="reason">The reason that the player was banned.</param>
        public void Ban([NotNull] string reason)
        {
            NetworkManager.instance.Ban(this.ID, reason);
        }
        #endregion
    }
}