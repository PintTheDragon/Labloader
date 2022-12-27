using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using Labloader.Core.API.Enums;
using RiptideNetworking.Transports.RudpTransport;
using UnityEngine;

namespace Labloader.Core.API.Features
{
    public class Player : MonoBehaviour
    {
        #region STATIC
        private static List<Player> _list = new List<Player>();
        
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
        /// <returns>The Player, or null if they don't exist.</returns>
        public static Player Get(GameObject obj) => List.FirstOrDefault(ply => ply.GameObject == obj);
        #endregion

        #region COMPONENT
        private NetworkPlayerObject player;

        void Awake()
        {
            this.player = this.gameObject.GetComponent<NetworkPlayerObject>();
        }
        
        void OnEnable()
        {
            _list = Object.FindObjectsOfType<Player>().ToList();
        }
        
        void OnDisable()
        {
            _list = Object.FindObjectsOfType<Player>().ToList();
        }
        
        void OnDestroy()
        {
            _list = Object.FindObjectsOfType<Player>().ToList();
        }
        #endregion

        #region APIProps

        private new GameObject gameObject => base.gameObject;
        
        /// <summary>
        /// Gets the player's NetworkPlayerObject.
        /// </summary>
        public NetworkPlayerObject NetworkPlayer => this.player;
        
        /// <summary>
        /// Gets the player's GameObject.
        /// </summary>
        public GameObject GameObject => this.gameObject;

        /// <summary>
        /// The user's client ID (unique and specific to this server/session).
        /// </summary>
        public ushort ID => this.NetworkPlayer.netObj.ClientId;
        
        /// <summary>
        /// Gets the player's user ID (their ID through steam/discord), or null if they don't have one.
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
        public RoomIdentity.ZoneType Dimension => this.NetworkPlayer.GetZone();

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        public string Name => this.NetworkPlayer.PlayerName;

        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        public float Health => this.NetworkPlayer.stats.health.health;

        /// <summary>
        /// Damages the player.
        /// </summary>
        /// <param name="damage">The amount of damage to deal.</param>
        public void Damage(float damage)
        {
            // TODO: Message and killer.
            this.NetworkPlayer.stats.health.Damage(damage);
        }

        /// <summary>
        /// Gets the player's current role.
        /// </summary>
        public Role Role =>
            this.NetworkPlayer.stats.health.dead 
                ? Role.Spectator 
                : (RoleMap.TryGetValue(this.NetworkPlayer.CurrentCharacter.name, out var role) ? role : Role.None);
        
        /// <summary>
        /// Gets the player's current team.
        /// </summary>
        public Team Team =>
            this.NetworkPlayer.stats.health.dead 
                ? Team.Dead 
                : (TeamMap.TryGetValue(this.NetworkPlayer.CurrentTeam.name, out var team) ? team : Team.None);

        /// <summary>
        /// Gets the player's IP address, or null if none exists.
        /// </summary>
        public string IP => !((RudpServer)NetworkManager.instance.server.server).TryGetClient(this.NetworkPlayer.netObj.ClientId, out var client) ? null : client.RemoteEndPoint.Address.ToString();

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
        /// Gets or sets the player's walk speed.
        /// </summary>
        public float WalkSpeed
        {
            get => this.NetworkPlayer.stats.data.walkSpeed;
            set => this.NetworkPlayer.stats.data.walkSpeed = value;
        }

        /// <summary>
        /// Gets or sets the player's sprint speed.
        /// </summary>
        public float SprintSpeed
        {
            get => this.NetworkPlayer.stats.data.sprintSpeed;
            set => this.NetworkPlayer.stats.data.sprintSpeed = value;
        }

        /// <summary>
        /// Gets or sets the player's crouch speed.
        /// </summary>
        public float CrouchSpeed
        {
            get => this.NetworkPlayer.stats.data.crouchSpeed;
            set => this.NetworkPlayer.stats.data.crouchSpeed = value;
        }

        /// <summary>
        /// Gets or sets whether or not the player is noclipping.
        /// </summary>
        public bool Noclip
        {
            get => this.NetworkPlayer.NoclipController.enabled;
            set => this.NetworkPlayer.NoclipController.enabled = value;
        }
        #endregion

        #region APIMethods
        /// <summary>
        /// Kills the player.
        /// </summary>
        public void Kill(string message = null)
        {
            // TODO: Message, Killer.
            this.NetworkPlayer.stats.health.Kill();
        }

        /// <summary>
        /// Kick a player from the server.
        /// </summary>
        /// <param name="message">The message to be sent to the player.</param>
        public void Kick(string message = "You have been kicked from the server!")
        {
            // TODO: Kick messages.
            NetworkManager.instance.Kick(this.NetworkPlayer.netObj.ClientId);
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

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj != null && obj is Player p && p.GameObject.Equals(this.GameObject);
        }

        public static bool operator ==(Player obj1, object obj2)
        {
            return !(obj1 is null) && obj1.Equals(obj2);
        }

        public static bool operator !=(Player obj1, object obj2)
        {
            return !(obj1 == obj2);
        }
        #endregion
    }
}