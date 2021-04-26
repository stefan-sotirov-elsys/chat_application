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
        public string current_chat_room;
        public IPEndPoint remote_end_point;
        public Socket socket;

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
    }
}