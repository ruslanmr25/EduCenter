using System;

namespace Frontend.Extensions;

public static class IntExtensions
{
    public static string ToReadebleString(this int value)
    {
        return string.Format("{0:N0}", value).Replace(",", " ");
    }
}
