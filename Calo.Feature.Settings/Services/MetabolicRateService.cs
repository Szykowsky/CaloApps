
using Calo.Core.Entities;
using Calo.Domain.Entities.MetabolicRate;

namespace Calo.Feature.MetabolicRate.Services;

public class MetabolicRateService : IMetabolicRateService
{
    public int CalculateBMR(Gender gender, Formula formula, int weight, int growth, int age) =>
        gender switch
        {
            Gender.Male => PrepareMaleBMR(formula, weight, growth, age),
            Gender.Female => PrepareFemaleBMR(formula, weight, growth, age),
        };

    public int PrepareMaleBMR(Formula formula, int weight, int growth, int age) =>
        formula switch
        {
            Formula.HarrisBenedict => Convert.ToInt32(88.362f + (13.397f * weight) + (4.799f * growth) + (5.677f * age)),
            Formula.MifflinStJeor => MifflinStJeorBMR(weight, growth, age, 5)
        };

    public int PrepareFemaleBMR(Formula formula, int weight, int growth, int age) =>
        formula switch
        {
            Formula.HarrisBenedict => Convert.ToInt32(447.593f + (9.247f * weight) + (3.098f * growth) + (4.330f * age)),
            Formula.MifflinStJeor => MifflinStJeorBMR(weight, growth, age, -161)
        };

    public int MifflinStJeorBMR(int weight, int growth, int age, int genderValue) =>
        Convert.ToInt32((10 * weight) + (6.25f * growth) - (5 * age) + genderValue);

    public int CalculateAMR(int bmr, Activity activity) =>
        activity switch
        {
            Activity.Sedentary => Convert.ToInt32(bmr * 1.2),
            Activity.LightlyActive => Convert.ToInt32(bmr * 1.375),
            Activity.ModeratelyActive => Convert.ToInt32(bmr * 1.55),
            Activity.Active => Convert.ToInt32(bmr * 1.725),
            Activity.VeryActive => Convert.ToInt32(bmr * 1.9),
        };
}
