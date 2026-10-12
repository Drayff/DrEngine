using System.Numerics;

namespace Dr.Core.Components;

public struct Transform2D(Vector2 position, float rotation = 0f)
{
    public Vector2 Position = position;
    public float Rotation = rotation;
}