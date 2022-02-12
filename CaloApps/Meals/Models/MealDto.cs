namespace CaloApps.Meals.Models
{
    public class MealDto
    {
        public Guid Id { get; set; }
        public int Kcal { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
