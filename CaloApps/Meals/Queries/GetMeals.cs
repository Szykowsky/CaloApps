using CaloApps.Data;
using CaloApps.Meals.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CaloApps.Meals.Queries
{
    public class GetMeals
    {
        public class Query : IRequest<IEnumerable<MealDto>>
        {
            public Guid DietId { get; set; }
            public MealsSearch? MealsSearchModel { get; set; }
        }

        public class MealDto
        {
            public Guid Id { get; set; }
            public int Kcal { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }

        }
        public class Handler : IRequestHandler<Query, IEnumerable<MealDto>>
        {
            private readonly CaloContext dbContext;

            public Handler(CaloContext dbContext)
            {
                this.dbContext = dbContext;
            }
            public async Task<IEnumerable<MealDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await this.dbContext.Meals
                    .Where(m => m.DietId == request.DietId)
                    .Select(m => new MealDto
                    {
                        Kcal = m.Kcal,
                        Name = m.Name,
                        Date = m.Date,
                        Id = m.Id
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
