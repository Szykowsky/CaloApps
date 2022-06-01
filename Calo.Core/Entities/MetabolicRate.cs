using System.ComponentModel.DataAnnotations.Schema;

namespace Calo.Core.Entities
{
    public class MetabolicRate : BaseEntity
    {
        public Gender Gender { get; set; }
        public Activity Activity { get; set; }
        public Formula  Formula { get; set; }
        public int Weight { get; set; }
        public int Growth { get; set; }
        public int Age { get; set; }
        public int BasalMetabolicRate { get; set; }
        public int ActiveMetabolicRate { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Activity
    {
        Sedentary,
        LightlyActive,
        ModeratelyActive,
        Active,
        VeryActive
    }

    public enum Formula
    {
        HarrisBenedict,
        MifflinStJeor
    }
}
