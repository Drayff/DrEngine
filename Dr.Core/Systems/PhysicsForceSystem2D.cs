using System.Numerics;
using Dr.Core.Components;

namespace Dr.Core.Systems;

public class PhysicsForceSystem2D : ISystem
{
    public void Update(World world, float deltaTime)
    {
        foreach (var entityId in world.Query<RigidBody2D, ForceAccumulator2D, Velocity2D>())
        {
            
            var entity = world.GetEntity(entityId);
            ref var rigidBody = ref world.GetComponent<RigidBody2D>(entity);
            ref var forceAccumulator = ref world.GetComponent<ForceAccumulator2D>(entity);

            if (rigidBody.InverseMass != 0f)
            {
                ref var velocity = ref world.GetComponent<Velocity2D>(entity);

                var aLinear = forceAccumulator.Force * rigidBody.InverseMass;
                var aAngular = forceAccumulator.Torque * rigidBody.InverseMass;

                velocity.Linear += aLinear * deltaTime;
                velocity.Linear *= MathF.Max(0, 1 - rigidBody.LinearDrag * deltaTime);
                velocity.Angular += aAngular * deltaTime * MathF.Max(0,1 - rigidBody.AngularDrag * deltaTime);
                velocity.Angular *= MathF.Max(0, 1 - rigidBody.AngularDrag * deltaTime);
            }

            forceAccumulator.Force = Vector2.Zero;
            forceAccumulator.Torque = 0f;
        }
    }
}