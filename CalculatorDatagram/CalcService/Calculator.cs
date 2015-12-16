using System;
using System.ServiceModel;
using Contracts;

namespace CalcService
{
    class Calculator: ICalculator
    {
        public void Add(Arguments args, string callbackAddress) //Methode gibt nichts zurück, da void => keine zeitliche Abhängigkeit
        {
            //Result result = new Result();
            //result.Value = args.Arg1 + args.Arg2;
            Result result = new Result { Value = args.Arg1 + args.Arg2 }; // gleiches wie oben
            Console.WriteLine("{0} + {1}", args.Arg1, args.Arg2);
            var binding = new NetMsmqBinding(NetMsmqSecurityMode.None);

            var address = new EndpointAddress(callbackAddress); //adresse der messaging queue

            var factory = new ChannelFactory<ICalculatorCallbackChannel>(binding, address); // erzeugt Kommunikationskanal
            var channel = factory.CreateChannel();

            try
            {
                channel.SetResult(result);
                channel.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Recover
            }

            // nachfolgendes wird aufgrund des neuen Interfaces nicht benötigt
            //var clientChannel = (IClientChannel) channel;
            //clientChannel.Dispose();
        }
    }
    // Alternative
    public interface ICalculatorCallbackChannel : ICalculatorCallback, IClientChannel
    {
        
    }
}