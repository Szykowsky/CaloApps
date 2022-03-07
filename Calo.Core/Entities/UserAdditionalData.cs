namespace Calo.Core.Entities
{
    public class UserAdditionalData : BaseEntity
    {
        public Gender Gender { get; set; }
        public int Weight { get; set; }
        public int Growth { get; set; }
        public int Age { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
