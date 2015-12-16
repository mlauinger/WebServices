using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace CalcClient
{

    public interface ICalculatorChannel : ICalculator, IClientChannel
    {
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: CalculatorCallback <callbackAddress>");
                return;
            }
            var host = StartCallbackHost();
            var address = new EndpointAddress("net.msmq://localhost/private/calculator");
            var binding = new NetMsmqBinding(NetMsmqSecurityMode.None);
            var factory = new ChannelFactory<ICalculatorChannel>(binding, address);
            var channel = factory.CreateChannel();
            try
            {
                channel.Add(new Arguments { Arg1 = 1, Arg2 = 2 }, args[0]);
                Console.WriteLine("Message sent");
                channel.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            Console.ReadLine();
            host.Close();
           
        }

        private static ServiceHost StartCallbackHost()
        {
            var callbackAddress = new EndpointAddress("net.msmq://localhost/private/calculatorcallback");
            var callbackBinding = new NetMsmqBinding(NetMsmqSecurityMode.None);

            var host = new ServiceHost(typeof(CalculatorCallback));
            host.AddServiceEndpoint(typeof (ICalculatorCallback), callbackBinding,
                "net.msmq://localhost/private/calculatorcallback");
            host.Open();
            return host;
        }
    }
}
