using Calo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.MetabolicRate.Services
{
    public interface IMetabolicRateService
    {
        int CalculateBMR(Gender gender, Formula formula, int weight, int growth, int age);
        int PrepareMaleBMR(Formula formula, int weight, int growth, int age);
        int PrepareFemaleBMR(Formula formula, int weight, int growth, int age);
        int MifflinStJeorBMR(int weight, int growth, int age, int genderValue);
        int CalculateAMR(int bmr, Activity activity);
    }
}
