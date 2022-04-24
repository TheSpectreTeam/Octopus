namespace DynamicEntity.Extensions
{
    internal static class MethodBuilderExtensions
    {
        internal static MethodBuilder GenerateGetMethodILCode(this MethodBuilder methodBuilder, FieldBuilder fieldBuilder)
        {
            using (var propertySetGenerator = new GroboIL(methodBuilder))
            {
                propertySetGenerator.Ldarg(0);
                propertySetGenerator.Ldfld(fieldBuilder);
                propertySetGenerator.Ret();
            }
            return methodBuilder;
        }

        internal static MethodBuilder GenerateSetMethodILCode(this MethodBuilder methodBuilder, FieldBuilder fieldBuilder)
        {
            using (var propertySetGenerator = new GroboIL(methodBuilder))
            {
                propertySetGenerator.Ldarg(0);
                propertySetGenerator.Ldarg(1);
                propertySetGenerator.Stfld(fieldBuilder);
                propertySetGenerator.Ret();
            }

            return methodBuilder;
        }
    }
}
