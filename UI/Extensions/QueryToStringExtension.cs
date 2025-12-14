using System;
using System.Web;

namespace UI.Extensions;

public static class QueryToStringExtension
{
    public static string ToQueryString(this object obj)
    {
        var properties = obj.GetType().GetProperties();
        var query = HttpUtility.ParseQueryString(string.Empty);

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj);

            if (value != null)
                query[prop.Name] = value.ToString();
        }

        return query.ToString() ?? string.Empty;
    }
}
