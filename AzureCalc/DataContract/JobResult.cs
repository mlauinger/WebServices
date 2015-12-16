using Microsoft.WindowsAzure.Storage.Table;

namespace DataContract
{
    public class JobResult : TableEntity
    {

        public JobResult(CalcJob job, double result) : base("x", job.Id.ToString())
        {
            Value1 = job.Value1;
            Value2 = job.Value2;
            Result = result;
        }

        public JobResult()
        {

        }

        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Result { get; set; }

    }
}