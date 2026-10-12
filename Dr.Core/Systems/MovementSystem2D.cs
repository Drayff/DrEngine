using Dr.Core.Components;

namespace Dr.Core.Systems;

public class MovementSystem2D : ISystem
{
    public void Update(World world, float deltaTime)
    {
        foreach (var entityId in world.Query<Transform2D, Velocity2D>())
        {
            var entity = world.GetEntity(entityId);
            ref var transform = ref world.GetComponent<Transform2D>(entity);
            ref var velocity = ref world.GetComponent<Velocity2D>(entity);

            transform.Position += velocity.Linear * deltaTime;
            transform.Rotation += velocity.Angular * deltaTime;
        }
    }
}