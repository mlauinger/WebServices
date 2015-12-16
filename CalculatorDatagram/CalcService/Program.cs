using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace CalcService
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof (Calculator));
            Console.WriteLine();
            host.Open();
            Console.WriteLine("Service started");
            Console.ReadLine();
            host.Close();
        }

        
    }
}
