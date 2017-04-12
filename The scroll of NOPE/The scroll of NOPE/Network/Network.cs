using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace The_scroll_of_NOPE.Network
{
    #region William
    public class Client
    {
        private int port;
        private string ipaddr;
        private TcpClient client;

        public bool Connected { get { return client.Connected; } }

        public Client(string a, int p)
        {
            this.port = p;
            this.ipaddr = a;

            try
            {
                client = new TcpClient(a, p);
                Console.WriteLine("Client started and connected");
            }
            catch (SocketException e)
            {
                LogError("Connection error: " + e.ErrorCode + ": " + e.Message);
                client = new TcpClient();
            }

        }

        public bool SendData(string data)
        {
            // try/catch for error "handling"
            try
            {
                NetworkStream stream = client.GetStream();

                if (client.Connected)
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
        private TcpListener listener; // a server to accept connections and data
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
            listener = new TcpListener(IPAddress.Any, p);
            listener.Start();
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
                TcpClient lClient = listener.AcceptTcpClient();
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
            listener.Stop();
        }
    }

    public class ReceivedDataEventArgs : EventArgs
    {
        public string Data { get; set; }
    }
    #endregion
}
