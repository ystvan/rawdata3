using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace RDJTPService
{
    public class Server
    {
        private const int portNumber = 5000;

        static void Main()
        {
            Console.Title = "RDJTPService";
            Console.ForegroundColor = ConsoleColor.Red;
            Start();
        }

        static void Start() 
        {
            var server = GetRDJTPServer();

            var categories = new List<Category> 
            { 
                new Category { Id = 1, Name = "Beverages" },
                new Category { Id = 2, Name = "Condiments" },
                new Category { Id = 3, Name = "Confections" },
            };

            while (true)
            {

            }
        }

        private static TcpListener GetRDJTPServer()
        {
            var server = new TcpListener(IPAddress.Loopback, portNumber);
            return server;
        }
    }
}
