namespace DynamicEntity.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string item)
        {
            var firstPartUpper = char.ToUpper(item[0]);
            var secondPartLower = item.Substring(1).ToLower();
            return $"{firstPartUpper}{secondPartLower}";
        }

        public static int GetValidIntProperty(this string item)
        {
            if (item == null) return 0;
            int.TryParse(item, out var value);
            return value;
        }

        public static DateTime GetValidDateTimeProperty(this string item)
        {
            if (item == null) return DateTime.MinValue;
            DateTime.TryParse(item, out var value);
            return value;
        }
    }
}
