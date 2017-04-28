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
        public string UserIP;
        public string UserName;
        public NetworkStream connection;
        TcpClient client;

        public Client()
        {
            GetUserIP();
            UserName = GetUserName();
        }


        public void GetUserIP()
        {
            Console.WriteLine("What is your IP Address?");
            UserIP = Console.ReadLine();

        }
        public void ConnectingToServer()
        {
            client = new TcpClient(UserIP, 8080);
            connection = client.GetStream();
            Console.WriteLine("Connected to server. Start chatting below");
        }

        public string GetUserName()
        {
            Console.Write("Enter your name: ");
            string UserName = Console.ReadLine();
            UserName = UserName + " ";
            return (UserName);
        }

        public void EnterMessage()
        {
            Task.Run(() => Receiving());
            string input = Console.ReadLine();
            byte[] message = Encoding.Unicode.GetBytes(input);
            connection.Write(message, 0, message.Length);
            string entry = Encoding.Unicode.GetString(message, 0, message.Length);            
            EnterMessage();
            Console.ReadKey();
        }

        public void Sending(string text)
        {
            byte[] message = Encoding.Unicode.GetBytes(text);
            connection.Write(message, 0, message.Length);
        }

        public void Receiving()
        {
            try
            {
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int data = connection.Read(buffer, 0, client.ReceiveBufferSize);
                string message = Encoding.Unicode.GetString(buffer, 0, data);
                Console.WriteLine(message);
                Receiving();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
