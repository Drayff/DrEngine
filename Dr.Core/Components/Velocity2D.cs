using System.Numerics;

namespace Dr.Core.Components;

public struct Velocity2D(Vector2 linear, float angular = 0f)
{
    public Vector2 Linear = linear;
    public float Angular = angular;
}