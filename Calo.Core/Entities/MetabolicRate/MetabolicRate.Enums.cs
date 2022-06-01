using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Domain.Entities.MetabolicRate
{
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
