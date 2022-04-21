using DynamicEntity.Extensions;
using DynamicEntity.Abstractions;
using Common.Models.DynamicEntity;

namespace DynamicEntity.Services
{
    public class DynamicEntityCreateService : IDynamicEntityCreateService
    {
        private IEnumerable<DynamicEntityModelProperty> _dynamicProperties;
        private readonly IDynamicTypeFactory _dynamicTypeFactory;

        public DynamicEntityCreateService(IDynamicTypeFactory dynamicTypeFactory)
        {
            _dynamicTypeFactory = dynamicTypeFactory;
        }

        public Type CreateTypeByDescription(DynamicEntityModel dynamicEntity)
        {
            _dynamicProperties = dynamicEntity.Properties;
            return _dynamicTypeFactory.GetTypeWithDynamicProperty(
                typeof(DynamicEntityModelBase),
                dynamicEntity.EntityName,
                _dynamicProperties);
        }

        public IEnumerable<object> AddValuesToDynamicObject(Type extendedType,
            IEnumerable<string[]> values) =>
            values
                .Select(value => GetObjectWithProperty(extendedType, value));

        private object GetObjectWithProperty(Type dynamicType, string[] objValues)
        {
            var obj = Activator.CreateInstance(dynamicType);
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var index = _dynamicProperties
                    .FirstOrDefault(i => i.PropertyName.Equals(property.Name))
                    .ValueIndex;

                switch (property.PropertyType.Name)
                {
                    case "Int32":
                        property.SetValue(obj, objValues[index].GetValidIntProperty());
                        break;
                    case "DateTime":
                        property.SetValue(obj, objValues[index].GetValidDateTimeProperty());
                        break;
                    default:
                        property.SetValue(obj, objValues[index]);
                        break;
                }
            }
            return obj;
        }
    }
}
