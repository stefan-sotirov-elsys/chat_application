using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using message;

namespace server
{
    class ChatRoom
    {
        public string name;
        public List<Socket> connections;

        public ChatRoom(string name)
        {
            this.name = name;

            connections = new List<Socket>();
        }

        public void send_message(Message message)
        {
            int i;
            byte[] data = Message.message_to_byte_array(message);

            for (i = 0; i < connections.Count; ++i)
            {
                connections[i].Send(data);
            }
        }

        
    }
}
