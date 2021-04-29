using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using message;
using System.Threading;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(start_logger));
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ip = host.AddressList[1];
            Server server = new Server(ip, 13000, 256);

            thread.Start(server);
            server.start(100);
        }

        static void start_logger(object server)
        {
            while (true)
            {
                Message received_message = ((Server)(server)).accept_message(1000);

                Console.WriteLine(received_message.content);
            }
        }
    }
}
