using Labloader.Core.API.Features;

namespace Labloader.Core.Events.EventArgs
{
    public class PlayerExecutingCommandEventArgs : System.EventArgs
    {
        public Player Player { get; }

        public string Command { get; }
        
        public string[] Args { get; }

        public string Response { get; set; } = null;

        public PlayerExecutingCommandEventArgs(Player player, string command, string[] args)
        {
            Player = player;
            Command = command;
            Args = args;
        }
    }
}