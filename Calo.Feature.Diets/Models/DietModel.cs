namespace Calo.Feature.Diets.Models
{
    public class DietModel
    {
        public class Basic
        {
            public string Name { get; set; }
            public int DayKcal { get; set; }
            public int? Carbohydrates { get; set; }
            public int? Fiber { get; set; }
            public int? Protein { get; set; }
            public int? Fats { get; set; }
            public int? Vitamins { get; set; }
            public int? Minerals { get; set; }
        }
    }
}
