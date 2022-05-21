namespace Repository.MongoDb.Helpers
{
    internal static class MongoGuard
    {
        /// <summary>
        /// Checking for the possibility of casting an object to ObjectId.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="argumentValue">The argument value.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentException"></exception>
        internal static void InvalidObjectId(this IGuardClause guardClause, object argumentValue, string argumentName)
        {
            var value = argumentValue as string ?? throw new ArgumentException(argumentName);
            var etaloneObjectIdLenth = new ObjectId().ToString().Length;
            if (!value.Length.Equals(etaloneObjectIdLenth)) throw new ArgumentException(argumentName);
        }
    }
}
