using ChatRoom2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom2
{
    class ChatLog : IChatLog
    {
        private List<string> Messages = new List<string>();

        public List<string> MessageLog
        {
            get
            {
                return Messages;
            }
            set
            {
                MessageLog = value;
            }
        }

        List<string> IChatLog.Messages
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ChatLog()
        {

        }
        public void Write(string input)
        {
            MessageLog.Add(input);
           
        }

    }


}
