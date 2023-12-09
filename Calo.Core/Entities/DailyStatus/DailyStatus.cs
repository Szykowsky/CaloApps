using Calo.Domain.Base;
using Calo.Core.Entities;

namespace Calo.Domain.Entities;

public class DailyStatus : BaseEntity, IAggregateRoot
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int KcalConsumed { get; set; }
    public int KcalRemaining { get; set; }
    public Guid UserId { get; set; }

    public virtual IList<Meal> Meals { get; set; }
    public virtual User User { get; set; }

    public DailyStatus()
    {

    }

    public DailyStatus(int kcalRemaining, Guid userId)
    {
        this.KcalConsumed = 0;
        this.KcalRemaining = kcalRemaining;
        this.UserId = userId;
    }

    public DailyStatus(Guid id, int day, int month, int year, int kcalConsumed, int kcalRemaining, Guid userId)
    {
        this.Id = id;
        this.CreatedDate = DateTime.Now;
        this.UserId = userId;
        this.Update(day, month, year, kcalConsumed, kcalRemaining);
    }

    public void Update(int day, int month, int year, int kcalConsumed, int kcalRemaining)
    {
        this.Day = day;
        this.Month = month;
        this.Year = year;
        this.KcalConsumed = kcalConsumed;
        this.KcalRemaining = kcalRemaining;
        this.SetModifiedDate();
    }

    public void UpdateKcal(int kcalConsumed, int kcalRemaining)
    {
        this.KcalConsumed = kcalConsumed;
        this.KcalRemaining = kcalRemaining;
        this.SetModifiedDate();
    }
}
