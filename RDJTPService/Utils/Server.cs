using RDJTP.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RDJTPService
{
    public class Server
    {
        private const int portNumber = 5000;

        public void Start()
        {
            var server = GetRDJTPServer();
            server.Start(); // Start listening for incoming connection requests


            while (true)
            {
                var client = server.AcceptTcpClient();

                Thread thread = new Thread(delegate ()
                {
                    var stream = client.GetStream();
                    var buffer = new byte[client.ReceiveBufferSize];

                    var array = stream.Read(buffer, 0, buffer.Length);

                    var msg = Encoding.UTF8.GetString(buffer, 0, array);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\t {msg} \n");
                    Console.ForegroundColor = ConsoleColor.Red;

                    stream.Close();
                });

                thread.Start();
                Console.WriteLine($"Handled by thread id: {thread.GetHashCode()}");
            }
        }

        private TcpListener GetRDJTPServer()
        {
            var server = new TcpListener(IPAddress.Loopback, portNumber);
            return server;
        }

        private List<Category> GetCategories()
        {
            var categories = new List<Category> 
            { 
                new Category { Id = 1, Name = "Beverages" },
                new Category { Id = 2, Name = "Condiments" },
                new Category { Id = 3, Name = "Confections" },
            };

            return categories;
        }
    }
}
