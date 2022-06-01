
namespace Calo.Domain.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public BaseEntity()
        {
            this.SetModifiedDate();
        }

        public void SetModifiedDate()
        {
            this.ModifiedDate = DateTime.Now;
        }
    }
}
