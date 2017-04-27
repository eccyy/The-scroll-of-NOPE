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

        public Client(string a, int p)
        {
            this.port = p;
            this.ipaddr = a;

            try
            {
                _internalClient = new TcpClient(a, p);
                Console.WriteLine("Client started and connected");
            }
            catch (SocketException e)
            {
                LogError("Connection error: " + e.ErrorCode + ": " + e.Message);
                _internalClient = new TcpClient();
            }

        }

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
                LogError("Write error: " + e.Message);
                return false;
            }
            catch (SocketException e)
            {
                LogError("Write error: " + e.ErrorCode + ": " + e.Message);
                return false;
            }
        }

        private void LogError(string e)
        {
            Console.WriteLine(e);
        }
    }

    public class Server
    {
        private TcpListener _internalServer; // a server to accept connections and data
        private Thread listenerThread; // a thread to handle incoming data, would interrupt all other operations otherwise
        // private List<string> connectedClients = new List<string>(); // list to keep track of connected clients
        private int port;
        public event EventHandler<ReceivedDataEventArgs> ReceivedData;

        // konstruktor, tar en string och en int
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
        private bool PasswordProtected;
        private string lobbyPassword;

        public LobbySession()
        {
            sessionID = IDGenerator.GenerateID();

            PasswordProtected = false;
        }

        public LobbySession(string password)
        {
            sessionID = IDGenerator.GenerateID();

            PasswordProtected = true;
            this.lobbyPassword = password;
        }

        public bool UserJoin(SessionNode node, string password = "")
        {
            if (!PasswordProtected) nodes.Add(node);
            else if (PasswordProtected && AuthorizeUser(password)) nodes.Add(node);
            else return false;

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
        protected string Username;
        protected LobbySession lobbySession;
        protected GameSession gameSession;
        protected ulong userID;

        public SessionUser(string username)
        {
            this.Username = username;
            userID = IDGenerator.GenerateID();
        }
    }

    public class SessionHost : SessionUser
    {
        public SessionHost(string username) : base(username)
        {

        }

        public void CreateSession(int port)
        {
            lobbySession = new LobbySession();
            server = new Server(port);
            server.ReceivedData += HandleIncomingData;
        }

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
        public SessionNode(string username) : base(username)
        {

        }

        public void JoinSession(string ip, int port)
        {
            if (lobbySession.UserJoin(this))
                client = new Client(ip, port);
            else
                MessageBox.Show("Coudn't join session");
        }

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
