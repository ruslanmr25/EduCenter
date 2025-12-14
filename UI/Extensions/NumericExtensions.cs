using System;
using System.Globalization;

namespace UI.Extensions;

public static class NumericExtensions
{
    private static readonly NumberFormatInfo SpacedNfi = new NumberFormatInfo
    {
        NumberGroupSeparator = " ",
        NumberDecimalSeparator = ".",
    };

    public static string ToSpacedIntString(this double value) =>
        ((long)value).ToString("N0", SpacedNfi) + " so'm";

    public static string ToSpacedIntString(this float value) =>
        ((long)value).ToString("N0", SpacedNfi) + " so'm";

    public static string ToSpacedIntString(this decimal value) =>
        ((long)value).ToString("N0", SpacedNfi) + " so'm";

    public static string ToSpacedIntString(this int value) =>
        (value).ToString("N0", SpacedNfi) + " so'm";
}
