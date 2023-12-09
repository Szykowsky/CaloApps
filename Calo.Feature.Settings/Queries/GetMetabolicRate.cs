using Calo.Data;
using Calo.Domain.Entities.MetabolicRate;
using Calo.Feature.MetabolicRate.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Calo.Feature.MetabolicRate.Queries;

public class GetMetabolicRate
{
    public class Query : IRequest<QueryResult>
    {
        public Guid UserId { get; set; }
    }

    public class QueryResult
    {
        public Guid Id { get; set; }
        public Gender Gender { get; set; }
        public Activity Activity { get; set; }
        public Formula Formula { get; set; }
        public int Weight { get; set; }
        public int Growth { get; set; }
        public int Age { get; set; }
        public int BasalMetabolicRate { get; set; }
        public int ActiveMetabolicRate { get; set; }
    }

    public class Handler : IRequestHandler<Query, QueryResult>
    {
        private readonly CaloContext dbContext;

        public Handler(CaloContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
           var metabolic = await this.dbContext.MetabolicRate
                .Where(x => x.UserId == request.UserId && x.IsActive)
                .Select(x => new QueryResult
                {
                    Id = x.Id,
                    Activity = x.Activity,
                    Age = x.Age,
                    Formula = x.Formula,
                    Gender = x.Gender,
                    Growth = x.Growth,
                    Weight = x.Weight,
                    ActiveMetabolicRate = x.ActiveMetabolicRate,
                    BasalMetabolicRate = x.BasalMetabolicRate
                })
                .FirstOrDefaultAsync(cancellationToken);

            return metabolic;
        }
    }
}
