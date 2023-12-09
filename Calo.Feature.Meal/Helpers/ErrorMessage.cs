
namespace Calo.Feature.Meals.Helpers;

public static class ErrorMessage
{
    public const string NotNullDietId = "You have to add diet id";
    public const string NotNullName = "You Have to add name of meal";
    public const string MaxLengthName = "Max length: 250 characters";
    public const string NotNullKcal = "You Have to add kcal";
    public const string GratherThanKcal = "Kcal must be grather than 0";
    public const string NotNullDateType = "You have to pass correct date type";
    public const string DateNumberRange = "Day may be from 1 to 31";
    public const string MonthNumberRange = "Day may be from 1 to 12";
}
