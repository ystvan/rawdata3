using RDJTP.Core;
using System;
using System.Collections.Generic;

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

        public static List<Category> GetCategories()
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
