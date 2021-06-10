using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using message;
using System.IO;

namespace server
{
    class ChatRoom
    {
        public string room_code;
        public List<Socket> connections;
        public FileStream file;

        public ChatRoom(string room_code)
        {
            this.room_code = room_code;

            connections = new List<Socket>();

            file = File.Open(room_code + ".txt", FileMode.Open);
        }

        public void update(Socket connection)
        {
            Message new_message;
            byte[] content_bytes = new byte[256];

            new_message.type = "content";

            new_message.room_code = room_code;

            file.Seek(0, SeekOrigin.Begin);

            while (file.Read(content_bytes, 0, 256) != 0)
            {
                byte[] data;

                new_message.content = Encoding.ASCII.GetString(content_bytes);

                data = Message.message_to_byte_array(new_message);

                Message.xor_crypt_bytes(data);

                connection.Send(data);
            }
        }

        public void send_message(Message message)
        {
            int i;
            byte[] data = Message.message_to_byte_array(message);

            file.Write(Encoding.ASCII.GetBytes(message.content));

            Message.xor_crypt_bytes(data);

            for (i = 0; i < connections.Count; ++i)
            {
                connections[i].Send(data);
            }
        }

        ~ChatRoom()
        {
            file.Close();
        }
    }
}
