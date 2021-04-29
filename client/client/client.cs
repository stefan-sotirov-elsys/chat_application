﻿// TODO(Stefan): manage the visibility of some of the fields

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
        Queue<Message> gateway_buffer; // serves as a communication point between this class and the user interface
        public bool return_handle; // for the accept function

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

            socket.Send(Message.message_to_byte_array(message));
        }

        public void send_message(Message new_message)
        {
            socket.Send(Message.message_to_byte_array(new_message));
        }

        public void receive_message()
        {
            byte[] received_bytes = new byte[max_message_size];
            Message received_message;

            socket.Receive(received_bytes);

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