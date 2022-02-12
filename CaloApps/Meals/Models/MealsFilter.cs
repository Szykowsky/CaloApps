namespace CaloApps.Meals.Models
{
    public class MealsFilter
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
