using Dr.Core.Components;

namespace Dr.Core.Systems;

public class CollisionSystem2D : ISystem
{
    public void Update(World world, float deltaTime)
    {
        var entities = world.Query<Transform2D, BoxCollider2D>().ToList();

        for (var i = 0; i < entities.Count; i++)
        {
            for (var j = i + 1; j < entities.Count; j++)
            {
                var entityA = world.GetEntity(entities[i]);
                var entityB = world.GetEntity(entities[j]);
                
                // NEED TO REALIZE
            }
        }
    }
}