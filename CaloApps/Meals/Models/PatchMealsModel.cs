namespace CaloApps.Meals.Models
{
    public class PatchMealsModel
    {
        public Guid Id { get; set; }
        public int Kcal { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid DietId { get; set; }
    }
}
