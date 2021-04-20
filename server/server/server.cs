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
            List<Socket> accepter_sockets = new List<Socket>();

            listener_socket.Bind(local_end_point);
            Console.WriteLine("server started successfully");

            listener_socket.Listen(100);

            while (true)
            {
                byte[] received_bytes = new byte[256];
                Message message;
                object handle_message_args;
                Thread thread = new Thread(new ParameterizedThreadStart(handle_message_thread_wrapper));

                accepter_sockets.Add(listener_socket.Accept());
                accepter_sockets[accepter_sockets.Count - 1].Receive(received_bytes);

                message = Message.byte_array_to_message(received_bytes);
                
                handle_message_args = new object[2] { message, accepter_sockets[accepter_sockets.Count - 1] };

                thread.Start(handle_message_args);
            }
        }

        void handle_message_thread_wrapper(object args)
        {
            Array args_array = (Array)args;
            Message message = (Message)args_array.GetValue(0);
            Socket current_socket = (Socket)args_array.GetValue(1);

            handle_message(message, current_socket);
        }

        void handle_message(Message message, Socket current_socket)
        {
            Message new_message;

            switch (message.type)
            {
                case "connect":
                    Console.WriteLine("new connection from " + message.content);

                    break;

                case "room_create":
                    chat_rooms.Add(message.content, new ChatRoom(message.content));
                    Console.WriteLine("created chat room: " + message.content);

                    break;

                case "room_join":
                    if (chat_rooms.ContainsKey(message.content))
                    {
                        chat_rooms[message.content].connections.Add(current_socket);
                        new_message = new Message("success", null, null);
                    }
                    else
                    {
                        new_message = new Message("error", "chat room doesn't exist", null);
                    }
                    current_socket.Send(Message.message_to_byte_array(new_message));

                    break;

                case "send_content":
                    if (chat_rooms.ContainsKey(message.room_name)) // will move this check in the client app(or remove it completely)
                    {
                        chat_rooms[message.room_name].send_message(message);
                    }
                    else
                    {
                        new_message = new Message("error", "chat room doesn't exist", null);
                        current_socket.Send(Message.message_to_byte_array(new_message));
                    }

                    break;

                case "error":
                    Console.WriteLine("error: " + message.content);
                    
                    break;
            }
        }
    }
}
