using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom2
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatLog chatlog = new ChatLog();
            Server server = new Server(chatlog);
            server.Listening();
        }
    }
}
