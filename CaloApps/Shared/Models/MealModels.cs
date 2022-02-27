
namespace Calo.SharedModels
{
    public class MealModels
    {
        public class Basic
        {
            public Guid Id { get; set; }
            public int Kcal { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
        }

        public class Patch : Basic
        {
            public Guid DietId { get; set; }
        }

        public class Dto : Basic
        {

        }

        public class Filter
        {
            public DateType? DateType { get; set; }
            public int? DayNumber { get; set; }
            public int? MonthNumber { get; set; }
        }

        public enum DateType
        {
            None,
            Day,
            Month,
            DayMonth
        }
    }
}
