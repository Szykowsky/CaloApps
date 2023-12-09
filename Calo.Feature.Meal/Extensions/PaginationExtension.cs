using Calo.Core.Models;
using Calo.Feature.Meals.Models;
using Microsoft.EntityFrameworkCore;
using static Calo.Feature.Meals.Queries.GetMeals;

namespace Calo.Feature.Meals.Extensions;

public static class PaginationExtension
{
    public static async Task<QueryMealsResult> GetPagedResult(this IQueryable<MealModels.Dto> query, int page, int pageSize, CancellationToken cancellationToken)
    {
        var rowCount = query.Count();
        var skip = (page - 1) * pageSize;
        var result = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        var pageCount = (double)rowCount / pageSize;

        return new QueryMealsResult
        {
            PaginationBase = new PaginationBase
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = rowCount,
                PageCount = (int)Math.Ceiling(pageCount),
            },
            QueryResult = result
        };
    }
}
