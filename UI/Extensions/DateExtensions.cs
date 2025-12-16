namespace UI.Extensions;

public static class DateExtensions
{
    private static readonly string[] UzbekMonths =
    {
        "yanvar",
        "fevral",
        "mart",
        "aprel",
        "may",
        "iyun",
        "iyul",
        "avgust",
        "sentabr",
        "oktabr",
        "noyabr",
        "dekabr",
    };

    public static string ToUzbekMonth(this DateTime date)
    {
        string month = UzbekMonths[date.Month - 1];
        return $"{date.Year} {char.ToUpper(month[0]) + month[1..]}";
    }

    public static string ToUzbekDate(this DateTime date)
    {
        string month = UzbekMonths[date.Month - 1];
        return $"{date.Day}-{month}, {date.Year}";
    }

    public static string? ToUzbekDate(this DateTime? date)
    {
        return null;
    }

    public static string ToUzbekDate(this DateOnly date)
    {
        string month = UzbekMonths[date.Month - 1];
        return $"{date.Day}-{month}, {date.Year}";
    }

    public static string ToDaysLeft(this DateTime dateTime, DateTime endDate)
    {
        var diff = (endDate.Date - dateTime.Date).Days;

        if (diff > 0)
            return $"{diff} kun qoldi";
        else if (diff < 0)
            return $"{Math.Abs(diff)} kun o'tdi";
        else
            return "Bugun to'lov kuni";
    }
}
