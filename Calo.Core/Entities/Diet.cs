using Calo.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calo.Core.Entities
{
    public class Diet : BaseEntity
    {
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
        public IList<Meal> Meals { get; set; }
    }
}
