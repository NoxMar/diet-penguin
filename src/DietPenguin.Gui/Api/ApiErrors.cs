using DietPenguin.Core;

namespace DietPenguin.Gui.Api;

public static class ApiErrors
{
    public static Error NotSuccessStatusCode =
        new("API.UNEXPECTED_STATUS_CODE", "Unexpected status code indicating failure");

    public static Error DeserializationFailed =
        new("API.DESERIALIZATION_FAILED", "Status code indicated success but the contents were in unexpected format");

}