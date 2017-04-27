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
        public Dictionary<TcpClient, string> ChatUsers = new Dictionary<TcpClient, string>();


        public void ListenForClient()
        {
            TcpClient client;
            listen.Start();
            Console.WriteLine("Listening for incoming connections");
            client = listen.AcceptTcpClient();
            Console.WriteLine("Client has connected to server");
            ChatUsers.Add(client);
            Task.Run(() => ConvertMessage(client));
            ListenForClient();
        }

        public void ConvertMessage(TcpClient client)
        {
            string messagestring = "";
            NetworkStream networkstream = null;
            networkstream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = networkstream.Read(buffer, 0, client.ReceiveBufferSize);
            string messagebyte = Encoding.Unicode.GetString(buffer, 0, data);
            messagebyte.ToCharArray();
            messagestring = string.Concat(messagebyte)
            JoinChatRoom(messagestring, client);




            ConvertMessage(client);
        }
    }
}