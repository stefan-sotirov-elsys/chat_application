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
        Queue<Message> gateway_buffer;

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
            Message message = null;

            while (true)
            {
                try
                {
                    ((Socket)current_socket).Receive(buf);
                }
                catch (Exception exception)
                {
                    if (message.room_code != null)
                    {
                        chat_rooms[message.room_code].connections.Remove((Socket)current_socket);
                    }

                    Console.WriteLine(exception.Message);

                    return;
                }

                if (((Socket)current_socket).Poll(1, SelectMode.SelectRead) == false || ((Socket)current_socket).Available == 0)
                {
                    Console.WriteLine(((Socket)current_socket).RemoteEndPoint.ToString() + " : process forcibly stopped");

                    return;
                }

                Message.xor_crypt_bytes(buf);

                message = Message.byte_array_to_message(buf);
                
                handle_message(message, (Socket)current_socket);
            }
        }

        void handle_message(Message message, Socket current_socket)
        {
            Message new_message = null;
            byte[] new_message_bytes;

            switch (message.type)
            {
                case "connect":
                    gateway_buffer.Enqueue(new Message("success", "new connection from " + ((IPEndPoint)(current_socket.RemoteEndPoint)).Address, null));

                    break;

                case "create_room":
                    if (chat_rooms.ContainsKey(message.content))
                    {
                        new_message = new Message("error", "chat room already exists", message.content);
                    }
                    else
                    {
                        new_message = new Message("success", "chat room has been created: " + message.content, message.content);

                        chat_rooms.Add(message.content, new ChatRoom(message.content));
                        gateway_buffer.Enqueue(new_message);
                    }

                    break;

                case "join_room":
                    if (chat_rooms.ContainsKey(message.room_code))
                    {
                        chat_rooms[message.room_code].connections.Add(current_socket);

                        new_message = new Message("success", message.content + " joined the room", message.room_code);

                        chat_rooms[message.room_code].send_message(new_message);
                    }
                    else
                    {
                        new_message = new Message("error", "chat room doesn't exist", message.room_code);
                    }

                    break;

                case "content":
                    chat_rooms[message.room_code].send_message(message);

                    break;

                case "error":
                    gateway_buffer.Enqueue(new Message("error", "error: " + message.content, null));

                    break;
            }

            if (message.type != "error" && message.type != "content" && message.type != "connect")
            {
                new_message_bytes = Message.message_to_byte_array(new_message);

                Message.xor_crypt_bytes(new_message_bytes);

                current_socket.Send(new_message_bytes);
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
