using System.Numerics;

namespace Dr.Core.Components;

public struct BoxCollider2D(Vector2 size, Vector2 offset = default)
{
    public Vector2 Size = size;
    public Vector2 Offset = offset;
}