using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ServiceReference2.CalculatorClient("BasicHttpEndpoint1");
            client.ClientCredentials.UserName.UserName = "testuser";
            client.ClientCredentials.UserName.Password = "testuser";
            Console.WriteLine("Enter two numbers you want to add");
            double arg1 = Convert.ToDouble(Console.ReadLine());
            double arg2 = Convert.ToDouble(Console.ReadLine());
            var arguments = new ServiceReference2.Arguments
            {
                Arg1 = arg1,
                Arg2 = arg2
            };
            var result = client.Add(arguments);
            Console.WriteLine(result.Value);
            Console.ReadLine();
            client.Close();
        }
    }
}
