using System.Numerics;

namespace Dr.Core.Components;

public struct ForceAccumulator2D()
{
    public Vector2 Force = Vector2.Zero;
    public float Torque = 0f;
}