
namespace Calo.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public BaseEntity()
        {
            this.ModifiedDate = DateTime.Now;
        }
    }
}
