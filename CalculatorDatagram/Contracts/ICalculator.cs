using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Hosting;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Contracts
{
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract(IsOneWay = true)]
        void Add(Arguments args, string callbackAddress); // void da bei Datagram nicht auf Antwort gewartet werden soll
        // callbackAddress in der Add-Logik wird in der Praxis nicht verwenden, da single responsibility verletzt wird
    }
}
