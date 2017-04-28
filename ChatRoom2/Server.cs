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
    public  Dictionary<TcpClient, string> ChatUsers = new Dictionary<TcpClient, string>();
    public Queue<string> MessageQueue = new Queue<string>();
    public TcpListener listen = new TcpListener(IPAddress.Any, 8080);

        IChatLog chatlog;

        public Server(IChatLog chatlog)
    {
        this.chatlog = chatlog;
    }

    public void Listening()
    {
        TcpClient client;
        listen.Start();
        Console.WriteLine("Listening for clients");
        client = listen.AcceptTcpClient();
        Console.WriteLine("Client connected");
        Task.Run(() => HandleClient(client));
        Listening();
    }

    public void HandleClient(TcpClient client)
    {
        string message = "";
        NetworkStream stream = null;
        try
        {
            stream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = stream.Read(buffer, 0, client.ReceiveBufferSize);
            string convertedmessage = Encoding.Unicode.GetString(buffer, 0, data);
            convertedmessage.ToCharArray();

                //If new user inputting their name
            if (convertedmessage[convertedmessage.Length - 1] == ' ')
            {
                JoinChatroom(convertedmessage, client);
            }

            //If chat message
            else
            {
                message = string.Concat(convertedmessage);
                for (int i = 0; i < ChatUsers.Count(); i++)
                {
                    if (ChatUsers.ElementAt(i).Key == client)
                    {
                        message = ($"{ChatUsers.ElementAt(i).Value} says: {message}");
                    }
                }
            }
        }

        catch (Exception)
        {
            if (stream == null)
            {
                return;
            }
        }
        WritingToChatLog(message);
        AddingToQueue(message);
        SendingMessage();
        HandleClient(client);
    }

        public void JoinChatroom(string convertedmessage, TcpClient client)
        {
            string username = string.Concat(convertedmessage);
            ChatUsers.Add(client, username);
            NotifyingOfJoin(username, client);
        }

    private void NotifyingOfJoin(string username, TcpClient client)
    {
        string notification = "";
        
        notification = ($"{username} joined");
       

        WritingToChatLog(notification);
        AddingToQueue(notification);
        SendingMessage();
    }
        public void SendingMessage()
        {
            byte[] message = Encoding.Unicode.GetBytes(DisplayingMessageInQueue());
            for (int i = 0; i < ChatUsers.Count(); i++)
            {
                try
                {
                    {
                        NetworkStream stream = ChatUsers.ElementAt(i).Key.GetStream();
                        stream.Write(message, 0, message.Length);
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }


        public void WritingToChatLog(string receivedMessage)
    {
        chatlog.Write(receivedMessage);
    }

    public void AddingToQueue(string receivedMessage)
    {
        MessageQueue.Enqueue(receivedMessage);
    }

    public string DisplayingMessageInQueue()
    {
        return MessageQueue.Dequeue();
    }


}
}
