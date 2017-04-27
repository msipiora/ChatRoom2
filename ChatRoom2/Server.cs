using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChatRoom2
{

    class Server
    {
        public TcpListener listen = new TcpListener(IPAddress.Any, 8080);


        public void ListenForClient()
        {
            TcpClient client;
            listen.Start();
            Console.WriteLine("Listening for incoming connections");
            client = listen.AcceptTcpClient();
            Console.WriteLine("Client has connected to server");
            Task.Run(() => ConvertMessage(client));
            ListenForClient();
        }

        public void ConvertMessage(TcpClient client)
        {

        }
    }
}