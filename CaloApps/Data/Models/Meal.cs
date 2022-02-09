using System.ComponentModel.DataAnnotations.Schema;

namespace CaloApps.Data.Models
{
    public class Meal
    {
        public Guid Id { get; set; }
        public int Kcal { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
