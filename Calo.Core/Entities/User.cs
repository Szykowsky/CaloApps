namespace Calo.Core.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public Guid SelectedDietId { get; set; }
    }
}
