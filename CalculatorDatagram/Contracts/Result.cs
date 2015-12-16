using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public double Value { get; set; }
    }
}