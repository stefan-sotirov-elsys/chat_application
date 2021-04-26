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
        IPEndPoint local_end_point;
        Dictionary<string, ChatRoom> chat_rooms;

        public Server(IPAddress server_ip, int port_number)
        {
            if (server_ip == null)
            {
                throw new ArgumentNullException("server_ip");
            }

            this.server_ip = server_ip;
            this.port_number = port_number;
            local_end_point = new IPEndPoint(server_ip, port_number);
            chat_rooms = new Dictionary<string, ChatRoom>();
        }

        public void start()
        {
            Socket listener_socket = new Socket(server_ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener_socket.Bind(local_end_point);
            Console.WriteLine("server started successfully");

            listener_socket.Listen(100);

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
            byte[] buf = new byte[256];
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
                    Console.WriteLine("new connection from " + message.content);

                    break;

                case "create_room":
                    chat_rooms.Add(message.content, new ChatRoom(message.content));
                    Console.WriteLine("chat room has been created: " + message.content);

                    break;

                case "join_room":
                    if (chat_rooms.ContainsKey(message.content))
                    {
                        chat_rooms[message.content].connections.Add(current_socket);
                    }
                    else
                    {
                        new_message = new Message("error", "chat room doesn't exist", null);
                        current_socket.Send(Message.message_to_byte_array(new_message));
                    }

                    break;

                case "leave_room":
                    chat_rooms[message.content].connections.Remove(current_socket);

                    break;

                case "content":
                    chat_rooms[message.room_name].send_message(message);

                    break;

                case "error":
                    Console.WriteLine("error: " + message.content);
                    
                    break;
            }
        }
    }
}
