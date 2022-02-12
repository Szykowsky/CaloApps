using System.ComponentModel.DataAnnotations.Schema;

namespace CaloApps.Data.Models
{
    public class Meal
    {
        public Guid Id { get; set; }
        public int Kcal { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid DietId { get; set; }

        [ForeignKey("DietId")]
        public virtual Diet Diet { get; set; }

    }
}
