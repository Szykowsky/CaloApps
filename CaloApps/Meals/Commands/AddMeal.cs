using CaloApps.Data;
using CaloApps.Data.Models;
using CaloApps.Shared.Models;
using MediatR;

namespace CaloApps.Meals.Commands
{
    public class AddMeal
    {
        public class Command: IRequest<RequestStatus>
        {
            public int Kcal { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
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
                var meal = new Meal
                {
                    Kcal = request.Kcal,
                    Name = request.Name,
                    Date = request.Date,
                    UserId = request.UserId,
                };

                await this.dbContext.AddAsync(meal, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Added new meal");
            }

        }
    }
}
