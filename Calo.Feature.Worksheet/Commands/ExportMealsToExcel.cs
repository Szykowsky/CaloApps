using Calo.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Calo.Feature.Worksheet.Commands
{
    public class ExportMealsToExcel
    {
        public class Command : IRequest<MemoryStream>
        {
            public Guid DietId { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Command, MemoryStream>
        {
            private readonly CaloContext dbContext;

            public Handler(CaloContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<MemoryStream> Handle(Command command, CancellationToken cancellationToken)
            {
                var meals = await this.dbContext.Meals
                    .Where(x => x.DietId == command.DietId && x.Diet.UserId == command.UserId)
                    .ToListAsync(cancellationToken);

                var stream = new MemoryStream();
                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("Meals");                  
                    //Create Headers and format them
                    using (var r = worksheet.Cells["A1:C1"])
                    {
                        r.Merge = true;
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                    }

                    worksheet.Cells["A4"].Value = "Date";
                    worksheet.Cells["B4"].Value = "Name";
                    worksheet.Cells["C4"].Value = "Kcal";
                    worksheet.Cells["A4:C4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A4:C4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells["A4:C4"].Style.Font.Bold = true;

                    const int startRow = 5;
                    var row = startRow;
                    foreach (var meal in meals)
                    {
                        worksheet.Cells[row, 1].Value = meal.Date.ToString(); // TODO prepare "nice" format of date
                        worksheet.Cells[row, 2].Value = meal.Name;
                        worksheet.Cells[row, 3].Value = meal.Kcal;

                        row++;
                    }

                    xlPackage.Save();
                }

                return stream;
            }
        }
    }
}
