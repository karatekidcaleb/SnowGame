using System;
using System.Numerics;
using Godot;
using Vector2 = Godot.Vector2;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void HitEventHandler();
    [Signal]
    public delegate void GrowEventHandler();
    [Signal]
    public delegate void ChangeLevelRequestEventHandler();

    [Export]
    public int Speed { get; set; } = 100; // How fast the player will move (pixels/sec).

    [Export]
    public bool waterable { get; set; } = true;
    public Vector2 ScreenSize { get; set; } // Size of the game window.

    private Vector2 velocity = Vector2.Zero;

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        velocity = Vector2.Right * 0;
        Show();
    }

    public override void _PhysicsProcess(double delta)
    {
        var collision = MoveAndCollide(velocity * (float)delta);
        if (collision != null)
        {
            if(((Node)collision.GetCollider()).Name.ToString().Contains("House"))
                waterable = true;
            else if (((Node)collision.GetCollider()).Name.ToString().Contains("Plant"))
            {
                EmitSignal(SignalName.Grow, waterable);
                waterable = false;
            }
            else if (((Node)collision.GetCollider()).Name.ToString().Contains("Door"))
            {
                EmitSignal(SignalName.ChangeLevelRequest);
            }
        }
    }

    public override void _Process(double delta)
    {
        velocity = Vector2.Zero;

        if (Input.IsActionPressed("move_right"))
            velocity.X += 1;
        if (Input.IsActionPressed("move_left"))
            velocity.X -= 1;
        if (Input.IsActionPressed("move_down"))
            velocity.Y += 1;
        if (Input.IsActionPressed("move_up"))
            velocity.Y -= 1;

        velocity = velocity.Normalized() * Speed;


        var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        if (velocity.Length() > 0)
            animatedSprite.Play();
        else
            animatedSprite.Animation = "up";

        if (velocity.X != 0)
        {
            animatedSprite.Animation = "right";
            animatedSprite.FlipH = velocity.X < 0;
        }
        else if (velocity.Y != 0)
        {
            animatedSprite.Animation = velocity.Y < 0 ? "up" : "down";
        }
    }
}