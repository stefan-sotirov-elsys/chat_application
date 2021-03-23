using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace message
{
    public class Message
    {
        public string type;
        public string content;
        public string room_name;

        public Message(string type, string content, string room_name)
        {
            this.type = type;
            this.content = content;
            this.room_name = room_name;
        }

        public static byte[] message_to_byte_array(Message message)
        {
            BinaryFormatter binary_formatter = new BinaryFormatter();
            MemoryStream memory_stream = new MemoryStream();

            binary_formatter.Serialize(memory_stream, message);
            return memory_stream.ToArray();
        }

        public static Message byte_array_to_message(byte[] bytes)
        {
            MemoryStream memory_stream = new MemoryStream();
            BinaryFormatter binary_formatter = new BinaryFormatter();
            Message message;

            memory_stream.Write(bytes, 0, bytes.Length);
            memory_stream.Seek(0, SeekOrigin.Begin);
            message = (Message)binary_formatter.Deserialize(memory_stream);
            return message;
        }
    }
}
