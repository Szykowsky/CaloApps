using Calo.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calo.Core.Entities
{
    public class Meal : BaseEntity, IAggregateRoot
    {
        public int Kcal { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid DietId { get; set; }

        [ForeignKey("DietId")]
        public virtual Diet Diet { get; set; }

        public Meal()
        {

        }

        public Meal(int kcal, string name, DateTime dateTime, Guid dietId)
        {
            this.Update(kcal, name, dateTime, dietId);
            this.CreatedDate = DateTime.Now;
        }

        public Meal(Guid id, int kcal, string name, DateTime dateTime, Guid dietId)
        {
            this.Id = id;
            this.Update(kcal, name, dateTime, dietId);
        }

        public void Update(int kcal, string name, DateTime dateTime, Guid dietId)
        {
            this.Kcal = kcal;
            this.Name = name;
            this.Date = dateTime;
            this.DietId = dietId;
            this.ModifiedDate = DateTime.Now;
        }

    }
}
