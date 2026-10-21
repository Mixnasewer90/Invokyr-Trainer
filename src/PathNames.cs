// Build: 94ce7e10a6ba759cee708cb3a1b27566
using System;
using System.Collections.Generic;
using System.Text;

internal static class PathNames
{
    public static string Slug(string value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        var result = new StringBuilder();
        bool separator = false;
        foreach (char character in value.Trim().ToLowerInvariant())
        {
            if (char.IsLetterOrDigit(character))
            {
                if (separator && result.Length > 0) result.Append('-');
                result.Append(character);
                separator = false;
            }
            else separator = true;
        }
        return result.ToString();
    }

    public static IReadOnlyList<string> UniqueSlugs(IEnumerable<string> labels)
    {
        var result = new List<string>();
        var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (string label in labels)
        {
            string stem = Slug(label);
            if (stem.Length == 0) continue;
            string candidate = stem;
            int suffix = 2;
            while (!used.Add(candidate)) candidate = stem + "-" + suffix++;
            result.Add(candidate);
        }
        return result;
    }
}
