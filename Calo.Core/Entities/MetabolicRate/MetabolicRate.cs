using Calo.Domain.Base;
using Calo.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calo.Domain.Entities.MetabolicRate
{
    public class MetabolicRate : BaseEntity, IAggregateRoot
    {
        public Gender Gender { get; set; }
        public Activity Activity { get; set; }
        public Formula Formula { get; set; }
        public int Weight { get; set; }
        public int Growth { get; set; }
        public int Age { get; set; }
        public int BasalMetabolicRate { get; set; }
        public int ActiveMetabolicRate { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public MetabolicRate(
            Gender gender,
            Activity activity,
            Formula formula,
            int weight,
            int growth,
            int age,
            int basalMetabolicRate,
            int activeMetabolicRate,
            Guid userId,
            bool isActive = true)
        {
            this.UserId = userId;
            this.CreatedDate = DateTime.Now;
            this.Update(gender, activity, formula, weight, growth, age, basalMetabolicRate, activeMetabolicRate, isActive);
        }

        public void Update(
            Gender gender,
            Activity activity,
            Formula formula,
            int weight,
            int growth,
            int age,
            int basalMetabolicRate,
            int activeMetabolicRate,
            bool isActive = true)
        {
            this.Gender = gender;
            this.Activity = activity;
            this.Formula = formula;
            this.Weight = weight;
            this.Growth = growth;
            this.Age = age;
            this.BasalMetabolicRate = basalMetabolicRate;
            this.ActiveMetabolicRate = activeMetabolicRate;
            this.IsActive = isActive;
            this.ModifiedDate = DateTime.Now;
        }

        public void SetIsActive(bool isActive)
        {
            this.IsActive = isActive;
        }
    }
}
