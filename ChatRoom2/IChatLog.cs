using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom2
{
    interface IChatLog
    {
        List<string> Messages { get; set; }

        void Write(string input); 
    }
}
