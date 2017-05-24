using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using The_scroll_of_NOPE.BaseClasses;
using The_scroll_of_NOPE.BaseClasses.Players;

namespace The_scroll_of_NOPE.Network
{
    #region William
    public abstract class SessionUser
    {
        protected NetworkInterface netiface;
        public string Username;
        protected LobbySession lobbySession;
        protected GameSession gameSession;
        protected ulong userID;
        // protected Player player;

        public ulong UserID { get { return this.userID; } set { this.userID = value; } }

        /// <summary>
        /// Constructor.
        /// Gives the user a username and an ID.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="port">Port of the host/server listening port.</param>
        public SessionUser(string username, int port)
        {
            this.Username = username;
            userID = IDGenerator.GenerateID();

            if (!netiface.IsInstantiated)
            {
                netiface = new NetworkInterface(port);
                netiface.ReceivedData += HandleIncomingData;
            }

        }

        /// <summary>
        /// Constructor.
        /// Gives the user a username and an ID.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="port">Port of the host/server listening port.</param>
        /// <param name="ip">IP address of the host.</param>
        public SessionUser(string username, int port, string ip) : this(username, port)
        {
            netiface = new NetworkInterface(port, ip);
            netiface.ReceivedData += HandleIncomingData;
        }

        /// <summary>
        /// Handle the incoming data.
        /// </summary>
        /// <param name="s">The sender object.</param>
        /// <param name="e">Event arguments</param>
        private void HandleIncomingData(object s, ReceivedDataEventArgs e)
        {

        }
    }

    public class SessionHost : SessionUser
    {

        /// <summary>
        /// Constructor.
        /// Gives the user a username and gives them an ID.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="port">Port of the host/server listening port.</param>
        public SessionHost(string username, int port) : base(username, port)
        {

        }

        /// <summary>
        /// Creates a new session.
        /// </summary>
        public void CreateNewSession()
        {
            lobbySession = new LobbySession();
        }

        /// <summary>
        /// Creates a password protected session.
        /// </summary>
        /// <param name="password">Password to protect the session with.</param>
        public void CreateNewSession(string password)
        {
            lobbySession = new LobbySession(password);
        }
    }

    public class SessionNode : SessionUser
    {
        /// <summary>
        /// Constructor.
        /// Gives the user a username and gives them an ID.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="ip">IP address of the host.</param>
        /// <param name="port">Port of the host/server listening port.</param>
        public SessionNode(string username, string ip, int port) : base(username, port, ip)
        {

        }

        /// <summary>
        /// Connects the user to a session.
        /// </summary>
        public void JoinSession()
        {
            lobbySession = GetHostSession();
            if (lobbySession.UserJoin(this))
            {
                // do stuff
            }
            else
                MessageBox.Show("Coudn't join session");
        }

        /// <summary>
        /// Connects the user to a session.
        /// </summary>
        /// <param name="password">Password for authentication.</param>
        public void JoinSession(string password)
        {
            lobbySession = GetHostSession();
            if (lobbySession.UserJoin(this, password))
            {
                // do stuff
            }
            else
                MessageBox.Show("Coudn't join session");
        }

        /// <summary>
        /// Does a "handshake" with the host to retrieve the lobby session data.
        /// </summary>
        /// <returns>The Host's lobby session data</returns>
        private LobbySession GetHostSession()
        {
            return new LobbySession(); // not how it's supposed to be, but I needed a return value because it throws an error otherwise
        }
    }
    #endregion
}
