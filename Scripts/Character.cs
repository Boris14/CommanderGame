using Godot;
using System;

public partial class Character : CharacterBody3D
{
    // How fast the player moves in meters per second.
    [Export]
    private float MaxSpeed = 6.0f;
    // The downward acceleration when in the air, in meters per second squared.
    [Export]
    private float Acceleration = 10.0f;
    [Export]
    private float FallAcceleratin = 75.0f;
    [Export]
    private float Friction = 10.0f;
    [Export]
    private PackedScene ControllerScene;

    private bool IsRunning = false;

    private AnimationPlayer AnimPlayer;
    private Skeleton3D Skeleton;

    public override void _Ready()
    {
        base._Ready();

        AnimPlayer = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        Skeleton = GetNodeOrNull<Skeleton3D>("Armature/Skeleton3D");

        if (ControllerScene != null)
        {
            Controller CharacterController = ControllerScene.Instantiate<Controller>();
            if(CharacterController != null)
            {
                CharacterController.SetCharacter(this);
                AddChild(CharacterController);
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector3 NewVelocity = Velocity;
        if (!IsOnFloor())
        {
            NewVelocity.Y -= FallAcceleratin * (float)delta;
        }
        else
        {
            NewVelocity.Y = 0;
        }

        Velocity = NewVelocity;
    }

    public void MoveTowards(Vector3 direction, double delta)
    {
        direction = direction.Normalized();

        if (direction.Abs() > Vector3.Zero)
        {
            Velocity = Velocity.Lerp(direction * MaxSpeed, (float)delta * Acceleration);
        }
        else
        {
            Velocity = Velocity.Lerp(Vector3.Zero, (float)delta * Friction);
        }

        if (!direction.IsZeroApprox() && !IsRunning)
        {
            AnimPlayer.Play("Running");
            Skeleton.ShowRestOnly = false;
            IsRunning = true;
        }
        else if(direction.IsZeroApprox() && IsRunning)
        {
            AnimPlayer.Stop();
            Skeleton.ShowRestOnly = true;
            IsRunning = false;
        }

        if(!direction.IsZeroApprox())
        {
            GetNode<Node3D>("Armature").Basis = Basis.LookingAt(direction);
        }

        MoveAndSlide();
    }
}
