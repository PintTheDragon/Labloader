using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using Labloader.API.Enums;
using RiptideNetworking.Transports.RudpTransport;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Labloader.API.Features
{
    public class Player : MonoBehaviour
    {
        private static List<Player> list = new List<Player>();
        
        /// <summary>
        /// Maps unity object name (PlayerCharacter.name) to role enum.
        /// </summary>
        private static Dictionary<string, Role> RoleMap = new Dictionary<string, Role>()
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
        private static Dictionary<string, Team> TeamMap = new Dictionary<string, Team>()
        {
            { "CBTeam", Team.ContainmentBreach },
            { "ClassDTeam", Team.ClassD },
            { "MTFTeam", Team.Mtf },
            { "SCPTeam", Team.Scp },
        };
        
        private NetworkPlayerObject player;

        public void Awake()
        {
            player = this.gameObject.GetComponent<NetworkPlayerObject>();
        }

        public void OnEnable()
        {
            list = Object.FindObjectsOfType<Player>().ToList();
        }
        
        public void OnDisable()
        {
            list = Object.FindObjectsOfType<Player>().ToList();
        }
        
        public void OnDestroy()
        {
            list = Object.FindObjectsOfType<Player>().ToList();
        }

        /// <summary>
        /// Gets all of the players in the server.
        /// </summary>
        public static ReadOnlyCollection<Player> List => list.AsReadOnly();

        /// <summary>
        /// Gets the player's gameobject.
        /// </summary>
        public GameObject GameObject => this.gameObject;

        /// <summary>
        /// Gets the player's dimension.
        /// </summary>
        public RoomIdentity.ZoneType Dimension => this.player.GetZone();

        /// <summary>
        /// Kills the player.
        /// </summary>
        public void Kill(string message = null)
        {
            // TODO: Message, Killer.
            this.player.stats.health.Kill();
        }

        /// <summary>
        /// Kick a player from the server.
        /// </summary>
        /// <param name="message">The message to be sent to the player.</param>
        public void Kick(string message = "You have been kicked from the server!")
        {
            // TODO: Kick messages.
            NetworkManager.instance.Kick(this.player.netObj.ClientId);
        }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        public string Name => this.player.PlayerName;

        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        public float Health => this.player.stats.health.health;

        /// <summary>
        /// Damages the player.
        /// </summary>
        /// <param name="damage">The amount of damage to deal.</param>
        public void Damage(float damage)
        {
            // TODO: Message and killer.
            this.player.stats.health.Damage(damage);
        }

        /// <summary>
        /// Gets the player's current role.
        /// </summary>
        public Role Role =>
            this.player.stats.health.dead 
                ? Role.Spectator 
                : (RoleMap.TryGetValue(this.player.CurrentCharacter.name, out var role) ? role : Role.None);
        
        /// <summary>
        /// Gets the player's current team.
        /// </summary>
        public Team Team =>
            this.player.stats.health.dead 
                ? Team.Dead 
                : (TeamMap.TryGetValue(this.player.CurrentTeam.name, out var team) ? team : Team.None);

        /// <summary>
        /// Gets the player's ID, or null if they don't have one.
        /// </summary>
        [CanBeNull]
        public string ID
        {
            get
            {
                var id = NetworkManager.instance.connectedPlayers[this.player.netObj.ClientId].authUserId;
                return string.IsNullOrEmpty(id) ? null : id;
            }
        }

        /// <summary>
        /// Gets the player's IP address, or null if none exists.
        /// </summary>
        public string IP => !((RudpServer)NetworkManager.instance.server.server).TryGetClient(this.player.netObj.ClientId, out var client) ? null : client.RemoteEndPoint.Address.ToString();

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
            get => this.player.stats.data.walkSpeed;
            set => this.player.stats.data.walkSpeed = value;
        }

        /// <summary>
        /// Gets or sets the player's sprint speed.
        /// </summary>
        public float SprintSpeed
        {
            get => this.player.stats.data.sprintSpeed;
            set => this.player.stats.data.sprintSpeed = value;
        }

        /// <summary>
        /// Gets or sets the player's crouch speed.
        /// </summary>
        public float CrouchSpeed
        {
            get => this.player.stats.data.crouchSpeed;
            set => this.player.stats.data.crouchSpeed = value;
        }

        /// <summary>
        /// Gets or sets whether or not the player is noclipping.
        /// </summary>
        public bool Noclip
        {
            get => this.player.NoclipController.enabled;
            set => this.player.NoclipController.enabled = value;
        }

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
    }
}