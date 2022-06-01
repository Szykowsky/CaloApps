using Calo.Core.Models;
using Calo.Feature.MetabolicRate.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.MetabolicRate.Commands
{
    public class UpdateMetabolicRate
    {
        public class Command : MetabolicRateModel.CreateOrUpdateModel, IRequest<RequestStatus>
        {

        }
    }
}
