using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RDJTPClient
{
    class Cilent
    {
        private const int portNumber = 5000;

        static void Main()
        {
            Console.Title = "RDJTPClient";
            Console.ForegroundColor = ConsoleColor.Green;
            Start();
        }

        static void Start()
        {
            var client = GetRDJTPClient();
        }

        private static TcpClient GetRDJTPClient()
        {
            var client = new TcpClient();
            client.Connect(IPAddress.Loopback, portNumber);

            return client;
        }
    }
}
