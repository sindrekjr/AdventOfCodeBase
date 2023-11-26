namespace AdventOfCode.Solutions.Utils;

public static class StringUtils
{
    public static string Reverse(this string str)
    {
        char[] arr = str.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    public static string[] SplitByNewline(this string str, bool shouldTrim = false) => str
        .Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None)
        .Where(s => !string.IsNullOrWhiteSpace(s))
        .Select(s => shouldTrim ? s.Trim() : s)
        .ToArray();

    public static string[] SplitByParagraph(this string str, bool shouldTrim = false) => str
        .Split(new[] { "\r\r", "\n\n", "\r\n\r\n" }, StringSplitOptions.None)
        .Where(s => !string.IsNullOrWhiteSpace(s))
        .Select(s => shouldTrim ? s.Trim() : s)
        .ToArray();

    public static int[] ToIntArray(this string str, string delimiter = "")
    {
        if (delimiter == "")
        {
            var result = new List<int>();
            foreach (char c in str) if (int.TryParse(c.ToString(), out int n)) result.Add(n);
            return [.. result];
        }
        else
        {
            return str
                .Split(delimiter)
                .Where(n => int.TryParse(n, out int v))
                .Select(n => Convert.ToInt32(n))
                .ToArray();
        }
    }

    public static long[] ToLongArray(this string str, string delimiter = "")
    {
        if (delimiter == "")
        {
            var result = new List<long>();
            foreach (char c in str) if (long.TryParse(c.ToString(), out long n)) result.Add(n);
            return [.. result];
        }
        else
        {
            return str
                .Split(delimiter)
                .Where(n => long.TryParse(n, out long v))
                .Select(n => Convert.ToInt64(n))
                .ToArray();
        }
    }
}
