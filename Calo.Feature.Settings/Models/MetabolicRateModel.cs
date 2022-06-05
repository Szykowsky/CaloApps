using Calo.Core.Entities;
using Calo.Domain.Entities.MetabolicRate;
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
            public Formula Formula { get; set; }
        }

        public class CreateModel : BaseModel
        {
            public Guid UserId { get; set; }
        }

        public class UpdateModel : BaseModel
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
        }

        public class Dto : BaseModel
        {
            public Guid Id { get; set; }
        }
    }
}
