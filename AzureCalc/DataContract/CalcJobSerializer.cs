using System;
using System.Xml;

namespace DataContract
{
    public class CalcJobSerializer
    {
        public static string Serialize(CalcJob job)
        {
            return string.Format("{0},{1},{2}", 
                job.Id, 
                XmlConvert.ToString(job.Value1), 
                XmlConvert.ToString(job.Value2));
        }

        public static CalcJob Deserialize(string asString)
        {
            var parts = asString.Split(',');
            return new CalcJob
            {
                Id = Guid.Parse(parts[0]), 
                Value1 = XmlConvert.ToDouble(parts[1]),
                Value2 = XmlConvert.ToDouble(parts[2])
            };
        }

       
    }
}