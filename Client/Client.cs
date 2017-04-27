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
            GetUserName();
        }

        public string GetUserName()
        {
            Console.WriteLine("Enter your username");
            UserName = Console.ReadLine();
            Console.WriteLine($"/n Welcome to chat {UserName}.");
            return (UserName);
        }

        public void UserInput()
        {
            string input = Console.ReadLine();
            byte[] chat = Encoding.Unicode.GetBytes(input);
            networkstream.Write(chat,0, chat.Length);
            Task.Run(() => ReceiveMessage());
            UserInput();
        }

        public void SendMessage(string input)
        {
            byte[] chat = Encoding.Unicode.GetBytes(input);
            networkstream.Write(chat, 0, chat.Length);
        }

        public void ReceiveMessage()
        {
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = networkstream.Read(buffer, 0, client.ReceiveBufferSize);
            string message = Encoding.Unicode.GetString(buffer, 0, data);
            Console.WriteLine(message);
            ReceiveMessage();
        }
    }
}