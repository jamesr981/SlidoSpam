namespace SlidoSpam.Extensions;

public static class StringExtensions
{
    public static string Truncate(this string val, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(val)) return val;
        return val.Length <= maxLength ? val : val[..maxLength];
    }
}