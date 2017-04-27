using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChatRoom2
{

    public class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.GetIPAddress();
            client.ConnectUser();
        }
    }
}
