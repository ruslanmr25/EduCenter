namespace Frontend.Services;

public static class DateTimeHelper
{
    private static readonly string[] _oylar =
    {
        "yanvar",
        "fevral",
        "mart",
        "aprel",
        "may",
        "iyun",
        "iyul",
        "avgust",
        "sentyabr",
        "oktabr",
        "noyabr",
        "dekabr",
    };

    public static string ToUzbekFormat(DateTime dateTime)
    {
        return $"{dateTime.Day}-{_oylar[dateTime.Month - 1]} {dateTime.Year} yil";
    }

    public static string ToUzbekFormat(DateOnly dateOnly)
    {
        return $"{dateOnly.Day}-{_oylar[dateOnly.Month - 1]} {dateOnly.Year} yil";
    }
}
