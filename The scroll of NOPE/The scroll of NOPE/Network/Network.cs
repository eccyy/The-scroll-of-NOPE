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
using Newtonsoft.Json;

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
                else
                {
                    Console.WriteLine("Coudn't send data, not connected to host.");
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

        /// <summary>
        /// Closes all connections and kills the client.
        /// </summary>
        public void StopClient()
        {
            _internalClient.Close();
        }

        /// <summary>
        /// Logs the error to console
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="code">Optional. The error code.</param>
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

        /// <summary>
        /// Listens for and handles all incoming transmissions.
        /// </summary>
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

    public class NetworkInterface
    {
        private Client client;
        private Server server;
        public event EventHandler<ReceivedDataEventArgs> ReceivedData;

        /// <summary>
        /// Constructor.
        /// Creates a new client and server.
        /// </summary>
        /// <param name="ipaddr">IP Address for the client.</param>
        /// <param name="port">Port for client and server.</param>
        public NetworkInterface(string ipaddr, int port)
        {
            client = new Client(ipaddr, port);
            server = new Server(port);
            //server.ReceivedData += HandleIncomingData;
            server.ReceivedData += ReceivedData;
        }

        /// <summary>
        /// Sends data to a server.
        /// </summary>
        /// <param name="data">Data to send.</param>
        /// <returns>A bool</returns>
        public bool SendData(string data)
        {
            return client.SendData(data);
        }

        /// <summary>
        /// Stops the server and the client.
        /// </summary>
        public void KillInterface()
        {
            server.StopServer();
            client.StopClient();
        }
    }

    #endregion
    #region Sessions

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
    #region SessionUsers

    public abstract class SessionUser
    {
        protected NetworkInterface netiface;
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
        /// <param name="ip">IP address of the host.</param>
        /// <param name="port">Port of the host/server listening port.</param>
        public SessionUser(string username, string ip, int port)
        {
            this.Username = username;
            userID = IDGenerator.GenerateID();
            netiface = new NetworkInterface(ip, port);
            netiface.ReceivedData += HandleIncomingData;
        }

        /// <summary>
        /// Handle the incoming data.
        /// </summary>
        /// <param name="s">The sender object.</param>
        /// <param name="e">Event arguments</param>
        protected void HandleIncomingData(object s, ReceivedDataEventArgs e)
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
        /// <param name="ip">IP address of the host.</param>
        /// <param name="port">Port of the host/server listening port.</param>
        public SessionHost(string username, string ip, int port) : base(username, ip, port)
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
        public SessionNode(string username, string ip, int port) : base(username, ip, port)
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
            return new LobbySession();
        }
    }

    #endregion
    public static class IDGenerator
    {
        /// <summary>
        /// Generates an unsigned int64 that can be used for ID's and similar.
        /// </summary>
        /// <param name="oldid">Optional. The old ID if any.</param>
        /// <returns>An unsigned int64</returns>
        public static ulong GenerateID(ulong oldid = 0)
        {
            var bytes = new byte[sizeof(UInt64)];
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider(); // Better rng than Random()
            Gen.GetBytes(bytes);
            ulong _internalID = BitConverter.ToUInt64(bytes, 0);

            return _internalID == oldid ? GenerateID(_internalID) : _internalID;
        }
    }
    #endregion
}
