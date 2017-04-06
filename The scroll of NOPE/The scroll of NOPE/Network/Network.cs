using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace The_scroll_of_NOPE.Network
{
    class Client
    {
        private int Port; // int för vilken nätverksport som ska användas
        private string Address; // string för vilken nätverksaddress som ska anslutas till
        private TcpClient client; // TcpClient, clienten som används för att skicka data

        // konstruktor, tar en string och en int
        public Client(string a, int p)
        {
            this.Port = p;
            this.Address = a;

            // creates a new client that connects to the host
            client = new TcpClient(a, p);
        }

        // metod för att skicka data
        // tar ett object
        public bool SendData(string data)
        {
            // try/catch for error "handling"
            try
            {
                NetworkStream stream = client.GetStream();

                if (client.Connected)
                {
                    byte[] d = System.Text.Encoding.ASCII.GetBytes(data, 0, data.Length);

                    stream.Write(d, 0, d.Length);
                }

                stream.Close();

                return true;
            }
            catch (ArgumentNullException e)
            {
                return false;
            }
            catch (SocketException e)
            {
                return false;
            }
        }

    }

    class Server
    {
        private TcpListener listener; // a server to accept connections and data
        private Thread listenerThread; // a thread to handle incoming data, would interrupt all other operations otherwise
        private delegate void GetData(string data); // delegate to handle events
        private List<string> connectedClients = new List<string>(); // list to keep track of connected clients
        private int Port;

        // konstruktor, tar en string och en int
        public Server(int p)
        {
            this.Port = p;

            // creates a server/listener that listens on any IP address
            listener = new TcpListener(IPAddress.Any, p);
            listener.Start();

            // creates the server thread and sets it up
            ThreadStart start = new ThreadStart(Listener);
            listenerThread = new Thread(start);
            listenerThread.IsBackground = true;
            listenerThread.Start();
        }

        private void Listener()
        {
            GetData dataDelegate = HandleData;
            
            while (true)
            {
                TcpClient lClient = listener.AcceptTcpClient();
                connectedClients.Add(IPAddress.Parse(((IPEndPoint)lClient.Client.RemoteEndPoint).Address.ToString()));
                NetworkStream stream = lClient.GetStream();
                byte[] bytes = new byte[256];
                var data = null;
                
                while ((i = stream.ReadByte(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    invoke(dataDelegate, data);
                }

                lClient.Close();
            } 
        }

        private void HandleData(string data)
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
        }

        public int Port { get { return this.Port; } }
    }
}
