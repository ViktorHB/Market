﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Authorization Service has just started. Hello.";

            ServiceHost serviceHost = new ServiceHost(typeof(Service));

            serviceHost.Open();

            Console.WriteLine("Для завершения нажмите <Any Key>.");
            Console.ReadKey();

            serviceHost.Close();
        }
    }
}
