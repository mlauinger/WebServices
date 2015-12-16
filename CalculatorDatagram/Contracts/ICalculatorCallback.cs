using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface ICalculatorCallback
    {
        [OperationContract(IsOneWay = true)]
        void SetResult(Result result);
    }
}