﻿using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalcService
{
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        Result Add(Arguments args);
    }
}
