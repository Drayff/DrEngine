using System;
using System.Diagnostics.CodeAnalysis;

namespace Dr.Core.Core;

public readonly struct Entity(int id, int generation) : IEquatable<Entity>
{
    public int Id { get; } = id;
    public int Generation { get; } = generation;

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Generation);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Entity other && Equals(other);
    }

    public bool Equals(Entity other)
    {
        return Id == other.Id && Generation == other.Generation;
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}