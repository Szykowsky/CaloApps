
namespace Calo.Feature.Meals.Models;

public class MealModels
{
    public class Basic
    {
        public Guid Id { get; set; }
        public int Kcal { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }

    public class RequestAdd : Basic
    {
        public Guid DietId { get; set; }
    }

    public class CreateOrUpdate : Basic
    {
        public Guid UserId { get; set; }
        public Guid DietId { get; set; }
    }

    public class Dto : Basic
    {

    }

    public class Filter
    {
        public int? DayNumber { get; set; }
        public int? MonthNumber { get; set; }
    }
}
