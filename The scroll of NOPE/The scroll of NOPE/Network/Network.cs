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
using The_scroll_of_NOPE.BaseClasses;
using The_scroll_of_NOPE.BaseClasses.Players;

namespace The_scroll_of_NOPE.Network
{
    #region William
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
        private bool isInstantiated = false;

        public bool IsInstantiated { get { return this.isInstantiated; } } // probably misspelled instantiated

        /// <summary>
        /// Constructor.
        /// Creates a new server.
        /// </summary>
        /// <param name="port">Port for client and server.</param>
        public NetworkInterface(int port)
        {
            server = new Server(port);
            server.ReceivedData += ReceivedData;
            isInstantiated = true;
        }

        /// <summary>
        /// Constructor.
        /// Creates a new client and server.
        /// </summary>
        /// <param name="port">Port for client and server.</param>
        /// <param name="ipaddr">IP Address for the client.</param>
        public NetworkInterface(int port, string ipaddr): this(port)
        {
            client = new Client(ipaddr, port);
        }

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ipaddr"></param>
        public void CreateNewClient(int port, string ipaddr)
        {
            if (client.Connected) client.StopClient();
            client = new Client(ipaddr, port);
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
