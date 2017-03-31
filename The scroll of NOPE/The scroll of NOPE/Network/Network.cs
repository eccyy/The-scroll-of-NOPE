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
    abstract class NetworkBase // Allt i denna fil skrivet av William
    {
        protected int Port; // int för vilken nätverksport som ska användas
        protected string Address; // string för vilken nätverksaddress som ska anslutas till
        protected TcpClient client; // TcpClient, clienten som används för att skicka data

        // metod för att skicka data
        // tar ett object
        public bool SendData(object data)
        {
            // try/catch for error "handling"
            try
            {
                NetworkStream stream = client.GetStream();

                if (client.Connected)
                {
                    string jsondata = JsonConvert.DeserializeObject<object>(data);
                    byte[] d = System.Text.Encoding.ASCII.GetBytes(jsondata, 0, jsondata.Length);

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

    class Node : NetworkBase
    {
        // konstruktor, tar en string och en int
        void Node(string a, int p)
        {
            this.Port = p;
            this.Address = a;

            // creates a new client that connects to the host
            client = new TcpClient(a, p);
        }
    }

    class Host : NetworkBase
    {
        private TcpListener listener; // a server to accept connections and data
        private Thread listenerThread; // a thread to handle incoming data, would interrupt all other operations otherwise
        private delegate void GetData(string data); // delegate to handle events
        private List<string> connectedClients = new List<string>(); // list to keep track of connected clients

        // konstruktor, tar en string och en int
        void Host(int p, string a)
        {
            this.Port = p;
            this.Address = a;

            // creates a new client to send data
            client = new TcpClient(a, p);
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
            object json = JsonConvert.SerializeObject<object>(data);
        }
    }
}
