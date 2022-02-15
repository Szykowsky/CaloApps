using CaloApps.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CaloApps.Diets.Queries
{
    public class GetDiets
    {
        public class Query : IRequest<IDictionary<Guid, string>>
        {
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IDictionary<Guid, string>>
        {
            private readonly CaloContext dbContext;

            public Handler(CaloContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<IDictionary<Guid, string>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await this.dbContext.Diets
                    .Where(x => x.UserId == request.UserId)
                    .ToDictionaryAsync(x => x.Id, x => x.Name);
            }
        }
    }
}
