using Godot;
using System;

public partial class PlayerController : Controller
{
    private Vector3 TargetVelocity = Vector3.Zero;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector3 direction = Vector3.Zero;

        if (Input.IsActionPressed("move_forward"))
        {
            direction.Z -= 1;
        }
        if (Input.IsActionPressed("move_back"))
        {
            direction.Z += 1;
        }
        if (Input.IsActionPressed("move_left"))
        {
            direction.X -= 1;
        }
        if (Input.IsActionPressed("move_right"))
        {
            direction.X += 1;
        }

        PossessedCharacter.MoveTowards(direction, delta);
    }
}
