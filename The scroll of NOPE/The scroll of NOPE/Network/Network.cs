using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.Cryptography;

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
        private delegate void GetData(string data); //delegate to handle events
        private List<string> connectedClients = new List<string>(); // list to keep track of connected clients
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
            //GetData dataDelegate = HandleData;

            while (true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient lClient = _internalServer.AcceptTcpClient();
                Console.WriteLine("Incoming connection from {0}", ((IPEndPoint)lClient.Client.RemoteEndPoint).Address.ToString());
                connectedClients.Add(((IPEndPoint)lClient.Client.RemoteEndPoint).Address.ToString());
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

                    //dataDelegate(data);
                }
                stream.Close();
                lClient.Close();
                Console.WriteLine("Connection closed");
            }
        }

        /*private void HandleData(string data)
        {
            foreach (string c in connectedClients)
            {
                TcpClient cli = new TcpClient(c, Port);
                NetworkStream stream = cli.GetStream();

                if (cli.Connected)
                {
                    byte[] d = System.Text.Encoding.ASCII.GetBytes(data, 0, data.Length);

                    stream.Write(d, 0, d.Length);
                }

                stream.Close();
            }
        }*/

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
        protected ulong sessionID;
        protected SessionHost sHost;
        protected SessionNode sNode;

        protected ulong GenerateID(ulong oldid = 0)
        {
            var bytes = new byte[sizeof(UInt64)];
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();
            Gen.GetBytes(bytes);
            ulong _internalID = BitConverter.ToUInt64(bytes, 0);

            return _internalID == oldid ? GenerateID(_internalID) : _internalID;
        }
    }

    public class LobbySession : NetworkSession
    {
        public LobbySession(string ip, int p, bool host = true)
        {
            if (host) sHost = new SessionHost(ip, p);
            else sNode = new SessionNode(ip, p);

            sessionID = GenerateID();
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

        public SessionUser(string ip, int p)
        {
            client = new Client(ip, p);
            server = new Server(p);
        }
    }

    public class SessionHost : SessionUser
    {
        public SessionHost(string ip, int p) : base(ip, p)
        {

        }
    }

    public class SessionNode : SessionUser
    {
        public SessionNode(string ip, int p) : base(ip, p)
        {

        }
    }

    #endregion
    #endregion
}
