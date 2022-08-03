using Entities;
using System.Linq;

/// <summary>
/// Entity factory.
/// </summary>
public class EntityFactory : MonoFactory<Entity>
{
    /// <summary>
    /// Creates an entity of the specified object type, if found.
    /// </summary>
    /// <param name="objectType">Entity type.</param>
    public override Entity CreateObject(ObjectType objectType)
    {
        Entity entity = objList.FirstOrDefault(x => x.EntityType.name == objectType.name);
        return Instantiate(entity);
    }
}
