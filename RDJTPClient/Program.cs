using System;
using System.Threading;

namespace RDJTPClient
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "RDJTPClient";
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < 5; i++)
            {
                var client = new Client();
                client.Start();
                Console.WriteLine($"Starting client {i}\n");
                Thread.Sleep(2000);
            }
            Console.WriteLine($"Press any key to terminate...");
            Console.ReadKey();
        }
    }
}
