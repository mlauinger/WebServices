using System;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading;

namespace CalcService
{
    public class Calculator : ICalculator
    {
        Result ICalculator.Add(Arguments args)
        {

            var requestIdentity = ServiceSecurityContext.Current.WindowsIdentity;
            var threadIdentity = WindowsIdentity.GetCurrent();
             
            requestIdentity.Impersonate();
            var threadIdentity2 = WindowsIdentity.GetCurrent();

            Console.WriteLine("request: {0}, thread: {1}", requestIdentity.Name, threadIdentity.Name);
            Console.WriteLine("request: {0}, thread: {1}", requestIdentity.Name, threadIdentity2.Name);

            Result result = new Result();
            result.Value = args.Arg1 + args.Arg2;
            return result;
        }
    }
}