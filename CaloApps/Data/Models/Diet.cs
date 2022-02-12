using System.ComponentModel.DataAnnotations.Schema;

namespace CaloApps.Data.Models
{
    public class Diet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DayKcal { get; set; }
        public int? Carbohydrates { get; set; }
        public int? Fiber { get; set; }
        public int? Protein { get; set; }
        public int? Fats { get; set; }
        public int? Vitamins { get; set; }
        public int? Minerals { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
