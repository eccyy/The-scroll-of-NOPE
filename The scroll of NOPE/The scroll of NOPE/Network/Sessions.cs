using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.Network
{
    #region William
    public abstract class NetworkSession
    {
        // ID might be useless on second thought, but I'll keep it in here for now.
        protected ulong sessionID;
        protected List<SessionNode> nodes = new List<SessionNode>();
    }

    public class LobbySession : NetworkSession
    {
        private bool passwordProtected = false;
        private string lobbyPassword;
        public event EventHandler SettingsChanged;

        public bool PasswordProtected { get { return this.passwordProtected; } }

        /// <summary>
        /// Constructor.
        /// Creates a new Lobby Session to handle all lobby events and users joining the game.
        /// </summary>
        /// <param name="state">The lobby state.</param>
        public LobbySession()
        {
            sessionID = IDGenerator.GenerateID();
        }

        /// <summary>
        /// Constructor.
        /// Creates a new password protected Lobby Session to handle all lobby events and users joining the game.
        /// </summary>
        /// <param name="password">Password to secure the lobby.</param>
        /// <param name="state">The lobby state.</param>
        public LobbySession(string password) : this()
        {
            passwordProtected = true;
            this.lobbyPassword = password;
        }

        /// <summary>
        /// Connects the user to the session.
        /// </summary>
        /// <param name="node">The SessionNode joining the session</param>
        /// <param name="password">Optional parameter. Password to authenticate the user.</param>
        /// <returns>A bool</returns>
        public bool UserJoin(SessionNode node, string password = "")
        {
            CheckUserId(node);

            if (!passwordProtected) nodes.Add(node);
            else if (passwordProtected && AuthorizeUser(password)) nodes.Add(node);
            else return false;

            return true;
        }

        /// <summary>
        /// Checks the new user's ID to the other connected clients so they don't collide
        /// </summary>
        /// <param name="node">The new user.</param>
        /// <returns>A bool</returns>
        private bool CheckUserId(SessionNode node)
        {
            foreach (SessionUser user in nodes)
            {
                if (node.UserID == user.UserID)
                {
                    node.UserID = IDGenerator.GenerateID(node.UserID);
                    CheckUserId(node);
                }
            }

            return true;
        }

        /// <summary>
        /// Compares the password the user entered to the lobbys password.
        /// </summary>
        /// <param name="password">The password from the user</param>
        /// <returns>A bool</returns>
        private bool AuthorizeUser(string password)
        {
            if (password == lobbyPassword) return true;
            else return false;
        }
    }

    public class GameSession : NetworkSession
    {
        public GameSession()
        {

        }
    }
    #endregion
}
