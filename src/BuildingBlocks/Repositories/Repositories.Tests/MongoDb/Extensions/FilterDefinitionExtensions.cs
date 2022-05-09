namespace Repositories.Tests.MongoDb.Extensions
{
    internal static class FilterDefinitionExtensions
    {
        internal static Func<I, bool> GetFunc<I>(this FilterDefinition<I> filter) where I : MongoEntityBase
        {
            if (filter is ExpressionFilterDefinition<I> f)
            {
                return f.Expression.Compile();
            }
            var fieldValue = filter.GetPrivateFieldValue<ExpressionFieldDefinition<I, bool>, I>("_field");
            return fieldValue.Expression.Compile();
        }

        internal static T GetPrivateFieldValue<T, I>(this FilterDefinition<I> filter, string fieldName) where I : MongoEntityBase
        {
            var fieldInfo = filter.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)fieldInfo.GetValue(filter);
        }
    }
}
