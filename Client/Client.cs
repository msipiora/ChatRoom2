using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChatRoom2

{
    class Client
    {
        public string UserName;
        public string UserIP;
        TcpClient client;
        public NetworkStream networkstream;

        public Client()
        {

        }

        public void GetIPAddress()
        {
            Console.WriteLine("Enter your IP Address");
            UserIP = Console.ReadLine();
        }

        public void ConnectUser()
        {
            client = new TcpClient(UserIP, 8080);
            networkstream = client.GetStream();
            Console.WriteLine("Connected to the server");
            Console.ReadLine();
        }
    }
}