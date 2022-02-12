using CaloApps.Data;
using CaloApps.Data.Models;
using CaloApps.Shared.Models;
using MediatR;

namespace CaloApps.Diets.Commands
{
    public class AddDiet
    {
        public class Command : IRequest<RequestStatus>
        {
            public string Name { get; set; }
            public int DayKcal { get; set; }
            public int? Carbohydrates { get; set; }
            public int? Fiber { get; set; }
            public int? Protein { get; set; }
            public int? Fats { get; set; }
            public int? Vitamins { get; set; }
            public int? Minerals { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Command, RequestStatus>
        {
            private readonly CaloContext dbContext;

            public Handler(CaloContext dbContext)
            {
                this.dbContext = dbContext;
            }
            public async Task<RequestStatus> Handle(Command request, CancellationToken cancellationToken)
            {
                var diet = new Diet
                {
                    Name = request.Name,
                    DayKcal = request.DayKcal,
                    Carbohydrates = request.Carbohydrates,
                    Fiber = request.Fiber,
                    Protein = request.Protein,
                    Fats = request.Fats,
                    Minerals = request.Minerals,
                    UserId = request.UserId,
                    Vitamins = request.Vitamins,
                };

                await this.dbContext.AddAsync(diet, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Added new diet");
            }
        }
    }
}
