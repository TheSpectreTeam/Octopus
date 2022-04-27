namespace DynamicEntity.Models
{
    internal class DynamicEntityMethodBuilder : DynamicEntityFieldBuilder
    {
        internal MethodAttributes MethodAttributes { get; }
        
        internal DynamicEntityMethodBuilder(TypeBuilder typeBuilder, 
            DynamicEntityModelProperty propertyField,
            MethodAttributes methodAttributes = MethodAttributes.Public | 
            MethodAttributes.SpecialName | 
            MethodAttributes.HideBySig)
            : base(typeBuilder, propertyField)
        {
            MethodAttributes = methodAttributes;
        }

        internal MethodBuilder GetMethodBuilder(string methodName, 
            Type[]? parameterTypes, 
            Type? returnType = null)
        {
            var methodBuilder = base.TypeBuilder.DefineMethod(
                returnType: returnType,
                name: GetMethodName(methodName),
                attributes: MethodAttributes,
                parameterTypes: parameterTypes);
            return methodBuilder;
        }

        private string GetMethodName(string methodName) 
            => $"{methodName.ToLower()}_{base.PropertyField.GetValidPropertyName()}";
    }
}
