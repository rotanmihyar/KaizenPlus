namespace kaizenplus.Extensions
{
    public static class StringExtension
    {
        public static bool HasValue(this string val)
        {
            return !string.IsNullOrEmpty(val);
        }
    }
}