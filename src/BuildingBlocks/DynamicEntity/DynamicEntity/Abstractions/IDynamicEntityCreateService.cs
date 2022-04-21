using Common.Models.DynamicEntity;

namespace DynamicEntity.Abstractions
{
    public interface IDynamicEntityCreateService
    {
        IEnumerable<object> AddValuesToDynamicObject(Type extendedType, IEnumerable<string[]> values);
        Type CreateTypeByDescription(DynamicEntityModel dynamicEntity);
    }
}
