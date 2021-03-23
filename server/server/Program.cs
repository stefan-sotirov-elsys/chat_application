using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ip = host.AddressList[0];

            Server server = new Server(ip, 13000);
            server.start();
        }
    }
}
