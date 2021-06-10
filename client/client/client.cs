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
        int max_message_size;
        Socket socket;
        Queue<Message> gateway_buffer;
        public bool return_handle;

        public Client(int max_message_size)
        {
            this.max_message_size = max_message_size;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            gateway_buffer = new Queue<Message>();
            return_handle = false;
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

            send_message(message);
        }

        public void send_message(Message new_message)
        {
            byte[] new_message_bytes = Message.message_to_byte_array(new_message);

            Message.xor_crypt_bytes(new_message_bytes);

            socket.Send(new_message_bytes);
        }
        
        public void receive_message()
        {
            byte[] received_bytes = new byte[max_message_size];
            Message received_message;

            socket.Receive(received_bytes);

            Message.xor_crypt_bytes(received_bytes);

            received_message = Message.byte_array_to_message(received_bytes);

            gateway_buffer.Enqueue(received_message);
        }

        public Message accept_message(int thread_sleep_time) // in milliseconds
        {
            while (gateway_buffer.Count == 0)
            {
                Thread.Sleep(thread_sleep_time);
            }

            if (return_handle)
            {
                return_handle = false;

                return new Message("", "", "");
            }

            return gateway_buffer.Dequeue();
        }
    }
}