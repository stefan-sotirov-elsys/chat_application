// TODO(Stefan): manage the visibility of some of the fields

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using message;

namespace client
{
    class Client
    {
        public string client_name;
        public string room_code;
        public IPEndPoint remote_end_point;
        public Socket socket;
        public Queue<Message> gateway_buffer = new Queue<Message>(); // serves as a communication point between this class and the user interface

        public void connect(IPEndPoint remote_end_point)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress local_ip = host.AddressList[1];
            Message message = new Message("connect", local_ip.ToString(), null);

            try
            {
                socket.Connect(remote_end_point);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            socket.Send(Message.message_to_byte_array(message));
        }

        public void send_message(string type, string content, string room_code)
        {
            Message new_message = new Message(type, content, room_code);

            socket.Send(Message.message_to_byte_array(new_message));
        }

        public void receive_messages()
        {
            byte[] received_bytes = new byte[256];
            Message received_message;

            while (true)
            {
                socket.Receive(received_bytes);

                received_message = Message.byte_array_to_message(received_bytes);

                gateway_buffer.Enqueue(received_message);
            }
        }

        public Message get_message()
        {
            while (gateway_buffer.Count == 0)
            {
                ;
            }

            return gateway_buffer.Dequeue();
        }
    }
}