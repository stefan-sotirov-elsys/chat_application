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
        public string room_code;
        public List<Socket> connections;

        public ChatRoom(string room_code)
        {
            this.room_code = room_code;

            connections = new List<Socket>();
        }

        public void send_message(Message message)
        {
            int i;
            byte[] data = Message.message_to_byte_array(message);

            for (i = 0; i < connections.Count; ++i)
            {
                try
                {
                    connections[i].Send(data);
                }
                catch
                {
                    connections.RemoveAt(i);
                }
            }
        }
    }
}
