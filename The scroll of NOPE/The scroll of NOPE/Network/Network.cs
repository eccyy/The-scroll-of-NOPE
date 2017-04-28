using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace The_scroll_of_NOPE.Network
{
    #region William
    #region Networking
    public class Client
    {
        private int port;
        private string ipaddr;
        private TcpClient _internalClient;

        public bool Connected { get { return _internalClient.Connected; } }

        /// <summary>
        /// Constructor.
        /// Connects the client to a server
        /// </summary>
        /// <param name="address"> The IP Address to connect to </param>
        /// <param name="port"> The network port to connect to </param>
        public Client(string address, int port)
        {
            this.port = port;
            this.ipaddr = address;

            try
            {
                _internalClient = new TcpClient(address, port);
                Console.WriteLine("Client started and connected");
            }
            catch (SocketException e)
            {
                LogError(e.Message, e.ErrorCode.ToString());
                _internalClient = new TcpClient();
            }

        }

        /// <summary>
        /// Opens a stream and sends data to a server
        /// </summary>
        /// <param name="data"> Data to send. My suggestion is a string parsed with Newtonsoft JSON. </param>
        /// <returns>A bool</returns>
        public bool SendData(string data)
        {
            // try/catch for error "handling"
            try
            {
                NetworkStream stream = _internalClient.GetStream();

                if (_internalClient.Connected)
                {
                    byte[] d = System.Text.Encoding.ASCII.GetBytes(data);
                    stream.Write(d, 0, d.Length);
                }

                stream.Close();

                return true;
            }
            catch (ArgumentNullException e)
            {
                LogError(e.Message);
                return false;
            }
            catch (SocketException e)
            {
                LogError(e.Message, e.ErrorCode.ToString());
                return false;
            }
        }

        private void LogError(string message, string code = "")
        {
            Console.WriteLine("Network Error: " + code + " " + message);
        }
    }

    public class Server
    {
        private TcpListener _internalServer; // a server to accept connections and data
        private Thread listenerThread; // a thread to handle incoming data, would interrupt all other operations otherwise
        // private List<string> connectedClients = new List<string>(); // list to keep track of connected clients
        private int port;
        public event EventHandler<ReceivedDataEventArgs> ReceivedData;

        /// <summary>
        /// Constructor.
        /// Starts a new thread for handling incoming connections
        /// </summary>
        /// <param name="p"> Port used to listen for incoming connections </param>
        public Server(int p)
        {
            this.port = p;

            // creates a server/listener that listens on any IP address
            _internalServer = new TcpListener(IPAddress.Any, p);
            _internalServer.Start();
            Console.WriteLine("Server started");

            // creates the server thread and sets it up
            ThreadStart start = new ThreadStart(Listener);
            listenerThread = new Thread(start);
            listenerThread.IsBackground = true;
            listenerThread.Start();
        }

        private void Listener()
        {
            while (true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient lClient = _internalServer.AcceptTcpClient();
                Console.WriteLine("Incoming connection from {0}", ((IPEndPoint)lClient.Client.RemoteEndPoint).Address.ToString());
                //connectedClients.Add(((IPEndPoint)lClient.Client.RemoteEndPoint).Address.ToString());
                NetworkStream stream = lClient.GetStream();
                byte[] bytes = new byte[256];
                string data = null;
                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = Encoding.ASCII.GetString(bytes, 0, i);

                    ReceivedDataEventArgs e = new ReceivedDataEventArgs();
                    e.Data = data;

                    ReceivedData?.Invoke(this, e); // that's a neat shortcut tbh
                }
                stream.Close();
                lClient.Close();
                Console.WriteLine("Connection closed");
            }
        }

        public int Port { get { return this.port; } }

        /// <summary>
        /// Stops the server and server thread
        /// </summary>
        public void StopServer()
        {
            listenerThread.Abort();
            _internalServer.Stop();
        }
    }

    public class ReceivedDataEventArgs : EventArgs
    {
        public string Data { get; set; }
    }

    #endregion
    #region Sessions

    public abstract class NetworkSession
    {
        // ID might be useless on second thought, but I'll keep it in here for now.
        protected ulong sessionID;
        protected List<SessionUser> nodes = new List<SessionUser>();
    }

    public class LobbySession : NetworkSession
    {
        private bool PasswordProtected = false;
        private string lobbyPassword;

        /// <summary>
        /// Constructor.
        /// Creates a new Lobby Session to handle all lobby events and users joining the game.
        /// </summary>
        public LobbySession()
        {
            sessionID = IDGenerator.GenerateID();
        }

        /// <summary>
        /// Constructor.
        /// Creates a new password protected Lobby Session to handle all lobby events and users joining the game.
        /// </summary>
        /// <param name="password">Password to secure the lobby.</param>
        public LobbySession(string password) : this()
        {
            PasswordProtected = true;
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

            if (!PasswordProtected) nodes.Add(node);
            else if (PasswordProtected && AuthorizeUser(password)) nodes.Add(node);
            else return false;

            return true;
        }

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
    #region SessionUsers

    public abstract class SessionUser
    {
        protected Client client;
        protected Server server;
        public string Username;
        protected LobbySession lobbySession;
        protected GameSession gameSession;
        protected ulong userID;

        public ulong UserID { get { return userID; } set { this.userID = value; } }

        /// <summary>
        /// Constructor.
        /// Gives the user a username and gives them an ID.
        /// </summary>
        /// <param name="username">The user's username.</param>
        public SessionUser(string username)
        {
            this.Username = username;
            userID = IDGenerator.GenerateID();
        }
    }

    public class SessionHost : SessionUser
    {
        /// <summary>
        /// Constructor.
        /// Gives the user a username and gives them an ID.
        /// </summary>
        /// <param name="username">The user's username.</param>
        public SessionHost(string username) : base(username)
        {

        }

        /// <summary>
        /// Creates a new session.
        /// </summary>
        /// <param name="port">Port for the session/server.</param>
        public void CreateSession(int port)
        {
            lobbySession = new LobbySession();
            server = new Server(port);
            server.ReceivedData += HandleIncomingData;
        }

        /// <summary>
        /// Creates a password protected session.
        /// </summary>
        /// <param name="port">Port for the session/server.</param>
        /// <param name="password">Password to protect the session with.</param>
        public void CreateSession(int port, string password)
        {
            lobbySession = new LobbySession(password);
            server = new Server(port);
            server.ReceivedData += HandleIncomingData;
        }

        private void HandleIncomingData(object s, ReceivedDataEventArgs e)
        {

        }
    }

    public class SessionNode : SessionUser
    {
        /// <summary>
        /// Constructor.
        /// Gives the user a username and gives them an ID.
        /// </summary>
        /// <param name="username">The user's username.</param>
        public SessionNode(string username) : base(username)
        {

        }

        /// <summary>
        /// Connects the user to a session.
        /// </summary>
        /// <param name="ip">IP Address to connect to.</param>
        /// <param name="port">Port to connect to.</param>
        public void JoinSession(string ip, int port)
        {
            if (lobbySession.UserJoin(this))
                client = new Client(ip, port);
            else
                MessageBox.Show("Coudn't join session");
        }

        /// <summary>
        /// Connects the user to a session.
        /// </summary>
        /// <param name="ip">IP Address to connect to.</param>
        /// <param name="port">Port to connect to.</param>
        /// <param name="password">Password for authentication.</param>
        public void JoinSession(string ip, int port, string password)
        {
          if (lobbySession.UserJoin(this, password))
              client = new Client(ip, port);
          else
              MessageBox.Show("Coudn't join session");
        }
    }

    #endregion
    public static class IDGenerator
    {
        /// <summary>
        /// Generates an unsigned int64 that can be used for ID's and similar.
        /// </summary>
        /// <param name="oldid"> The old id if any, optional </param>
        public static ulong GenerateID(ulong oldid = 0)
        {
            var bytes = new byte[sizeof(UInt64)];
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();
            Gen.GetBytes(bytes);
            ulong _internalID = BitConverter.ToUInt64(bytes, 0);

            return _internalID == oldid ? GenerateID(_internalID) : _internalID;
        }
    }
    #endregion
}
