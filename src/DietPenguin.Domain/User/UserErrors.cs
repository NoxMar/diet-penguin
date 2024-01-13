using DietPenguin.Core;

namespace DietPenguin.Domain.User;

public class UserErrors
{
    public static Error FutureDateOfBirthError = new Error(
        "USER.FUTURE_DATE_OF_BIRTH_ERROR",
        "Date of birth provided during user creation is after current date."
    );

    public static Error HeightLowerThanOrEquals0 = new Error(
        "USER.HEIGHT_LOWER_THAN_OR_EQUALS0", 
        "Value provided for the the height has to be greater than 0"
    );
}