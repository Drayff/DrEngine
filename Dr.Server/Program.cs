using System.Numerics;
using Dr.Core;
using Dr.Core.Components;
using Dr.Core.Systems;

Console.CursorVisible = false;

World world = new();

var entity1 = world.CreateEntity();
world.AddComponent(entity1, new Transform2D(new Vector2(0f, 10f)));
world.AddComponent(entity1, new  Velocity2D(new Vector2(0f, 0f)));
world.AddComponent(entity1, new ForceAccumulator2D());
world.AddComponent(entity1, new RigidBody2D(1f, 0.5f, 0f));

world.AddSystem(new PhysicsForceSystem2D());
world.AddSystem(new MovementSystem2D());

const int targetTickRate = 60;
const float physicsStep = 1.0f / targetTickRate;

for (var i = 0; i < 120000; i++)
{
    world.GetComponent<ForceAccumulator2D>(entity1).Force = new Vector2(0, -10);
    world.Update(physicsStep);
}

Console.WriteLine("Entity 1:");
Console.WriteLine($"- Position: {world.GetComponent<Transform2D>(entity1).Position}");
Console.WriteLine($"- Velocity: {world.GetComponent<Velocity2D>(entity1).Linear}");