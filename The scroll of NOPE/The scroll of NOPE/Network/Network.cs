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
    abstract class NetworkBase // Allt i denna fil skrivet av William
    {
        protected int Port; // int för vilken nätverksport som ska användas
        protected string Address; // string för vilken nätverksaddress som ska anslutas till
        protected TcpClient client; // TcpClient, clienten som används för att skicka data
    }

    class Node : NetworkBase
    {
        // konstruktor, tar en string och en int
        void Node(string a, int p)
        {
            this.Port = p;
            this.Address = a;

            client = new TcpClient(a, p);
        }
    }

    class Host : NetworkBase
    {
        private TcpListener listener;
        private Thread listenerThread;

        void Host(int p, string a)
        {
            this.Port = p;
            this.Address = a;

            client = new TcpClient(a, p);
            listener = new TcpListener(IPAddress.Any, p);

            ThreadStart start = new ThreadStart(Listener);
            listenerThread = new Thread(start);
            listenerThread.IsBackground = true;
            listenerThread.Start();
        }

        void Listener()
        {

        }
    }
}
