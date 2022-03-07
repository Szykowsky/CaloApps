using Calo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.MetabolicRate.Models
{
    public class MetabolicRateModel
    {
        public class BaseModel
        {
            public Gender Gender { get; set; }
            public int Weight { get; set; }
            public int Growth { get; set; }
            public int Age { get; set; }
            public Activity Activity { get; set; }
        }
    }
}
