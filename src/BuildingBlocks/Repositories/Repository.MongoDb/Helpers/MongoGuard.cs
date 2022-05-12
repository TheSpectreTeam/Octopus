namespace Repository.MongoDb.Helpers
{
    internal static class MongoGuard
    {
        internal static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null) 
                throw new ArgumentNullException(argumentName);
        }

        internal static void ArgumentIsObjectId(object argumentValue, string argumentName)
        {
            var value = argumentValue as string ?? throw new ArgumentException(argumentName);
            var etaloneObjectIdLenth = new ObjectId().ToString().Length;
            if (!value.Length.Equals(etaloneObjectIdLenth)) throw new ArgumentException(argumentName);
        }
    }
}
