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
        Socket socket;
        Queue<Message> gateway_buffer; // serves as a communication point between this class and the user interface

        public Client()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            gateway_buffer = new Queue<Message>();
        }

        public void connect(IPEndPoint remote_end_point)
        {
            Message message;

            try
            {
                socket.Connect(remote_end_point);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            message = new Message("connect", null, null);

            socket.Send(Message.message_to_byte_array(message));
        }

        public void send_message(Message new_message)
        {
            socket.Send(Message.message_to_byte_array(new_message));
        }

        public Message receive_message()
        {
            byte[] received_bytes = new byte[256];
            Message received_message;

            socket.Receive(received_bytes);

            received_message = Message.byte_array_to_message(received_bytes);

            return received_message;
        }

        public void listen_for_messages()
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

        public Message accept_message()
        {
            while (gateway_buffer.Count == 0)
            {
                Thread.Sleep(1000);
            }

            return gateway_buffer.Dequeue();
        }
    }
}