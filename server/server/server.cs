using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using message;

namespace server
{
    class Server
    {
        IPAddress server_ip;
        int port_number;
        int max_message_size;
        IPEndPoint local_end_point;
        Dictionary<string, ChatRoom> chat_rooms;
        Queue<Message> gateway_buffer; // serves as a communication point between the interface and this class

        public Server(IPAddress server_ip, int port_number, int max_message_size)
        {
            if (server_ip == null)
            {
                throw new ArgumentNullException();
            }

            this.server_ip = server_ip;
            this.port_number = port_number;
            this.max_message_size = max_message_size;
            local_end_point = new IPEndPoint(server_ip, port_number);
            chat_rooms = new Dictionary<string, ChatRoom>();
            gateway_buffer = new Queue<Message>();
        }

        public void start(int listener_socket_backlog)
        {
            Socket listener_socket = new Socket(server_ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener_socket.Bind(local_end_point);

            gateway_buffer.Enqueue(new Message("success", "server started successfully", null));

            listener_socket.Listen(listener_socket_backlog);

            while (true)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(handle_connection));
                Socket accepter_socket;

                accepter_socket = listener_socket.Accept();

                thread.Start(accepter_socket);
            }
        }

        void handle_connection(object current_socket)
        {
            byte[] buf = new byte[max_message_size];
            Message message;

            while (true)
            {
                try
                {
                    ((Socket)current_socket).Receive(buf);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);

                    return;
                }

                message = Message.byte_array_to_message(buf);

                
                handle_message(message, (Socket)current_socket);
            }
        }

        void handle_message(Message message, Socket current_socket)
        {
            Message new_message;

            switch (message.type)
            {
                case "connect":
                    gateway_buffer.Enqueue(new Message("success", "new connection from " + ((IPEndPoint)(current_socket.RemoteEndPoint)).Address, null));

                    break;

                case "create_room":
                    if (chat_rooms.ContainsKey(message.content))
                    {
                        new_message = new Message("error", "chat room already exists", message.content);
                        current_socket.Send(Message.message_to_byte_array(new_message));
                    }
                    else
                    {
                        new_message = new Message("success", "chat room has been created: " + message.content, message.content);

                        chat_rooms.Add(message.content, new ChatRoom(message.content));
                        gateway_buffer.Enqueue(new_message);

                        current_socket.Send(Message.message_to_byte_array(new_message));
                    }

                    break;

                case "join_room":
                    if (chat_rooms.ContainsKey(message.room_code))
                    {
                        chat_rooms[message.room_code].connections.Add(current_socket);

                        new_message = new Message("success", message.content + " joined the room", message.room_code);
                        current_socket.Send(Message.message_to_byte_array(new_message));

                        chat_rooms[message.room_code].send_message(new_message);
                    }
                    else
                    {
                        new_message = new Message("error", "chat room doesn't exist", message.room_code);
                        current_socket.Send(Message.message_to_byte_array(new_message));
                    }

                    break;

                case "content":
                    chat_rooms[message.room_code].send_message(message);

                    break;

                case "error":
                    gateway_buffer.Enqueue(new Message("error", "error: " + message.content, null));
                    
                    break;
            }
        }

        public Message accept_message(int thread_sleep_time) // in milliseconds
        {
            while (gateway_buffer.Count == 0)
            {
                Thread.Sleep(thread_sleep_time);
            }

            return gateway_buffer.Dequeue();
        }
    }
}
