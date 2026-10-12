namespace Dr.Core.Components;

public struct RigidBody2D
{
    public float Mass
    {
        get;
        set
        {
            field = value;
            InverseMass = field <= 0 ? 0f : 1f / field;
        }
    }

    public float InverseMass { get; private set; }
    public float LinearDrag;
    public float AngularDrag;
    public bool IsKinematic;

    public RigidBody2D()
    {
        Mass = 0f;
        LinearDrag = 0f;
        AngularDrag = 0f;
        IsKinematic = false;
    }

    public RigidBody2D(float mass, float linearDrag, float angularDrag)
    {
        Mass = mass;
        LinearDrag = linearDrag;
        AngularDrag = angularDrag;
        IsKinematic = true; 
    }
}