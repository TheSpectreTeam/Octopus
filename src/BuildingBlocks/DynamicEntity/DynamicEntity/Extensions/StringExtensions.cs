namespace DynamicEntity.Extensions
{
    public static class StringExtensions
    {
        public static string CapitalizedString(this string item)
        {
            var firstPartUpper = char.ToUpper(item[0]);
            var secondPartLower = item.Substring(1).ToLower();
            return $"{firstPartUpper}{secondPartLower}";
        }
    }
}
