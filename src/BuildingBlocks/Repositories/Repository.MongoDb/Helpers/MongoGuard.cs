namespace Repository.MongoDb.Helpers
{
    internal static class MongoGuard
    {
        internal static void IsObjectId(this IGuardClause guardClause, object argumentValue, string argumentName)
        {
            var value = argumentValue as string ?? throw new ArgumentException(argumentName);
            var etaloneObjectIdLenth = new ObjectId().ToString().Length;
            if (!value.Length.Equals(etaloneObjectIdLenth)) throw new ArgumentException(argumentName);
        }
    }
}
