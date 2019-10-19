using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RDJTPService
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "RDJTPService";
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"Server is starting...!\n");
            var server = new Server();
            server.Start();

        }
    }
}
