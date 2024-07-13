using Godot;

public partial class Player : Area2D
{
    [Signal]
    public delegate void HitEventHandler();
    [Signal]
    public delegate void GrowEventHandler();

    [Export]
    public int Speed { get; set; } = 100; // How fast the player will move (pixels/sec).

    [Export]
    public bool waterable { get; set; } = true;

    public Vector2 ScreenSize { get; set; } // Size of the game window.

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        Show();
    }

    public override void _Process(double delta)
    {
        var velocity = Vector2.Zero;

        if (Input.IsActionPressed("move_right"))
        {
            velocity.X += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            velocity.X -= 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            velocity.Y += 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            velocity.Y -= 1;
        }

        var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            animatedSprite.Play();
        }
        else
        {
            animatedSprite.Stop();
        }

        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );

        if (velocity.X != 0)
        {
            animatedSprite.Animation = "right";
            animatedSprite.FlipV = false;
            animatedSprite.FlipH = velocity.X < 0;
        }
        else if (velocity.Y != 0)
        {
            animatedSprite.Animation = velocity.Y < 0 ? "up" : "down";
        }
    }

    public void Start(Vector2 position)
    {
        Position = position;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    public void OnBodyEntered(Area2D body)
    {
        //Hide(); // Player disappears after being hit.
        EmitSignal(SignalName.Hit);
		GD.Print(body.Name.ToString());
		if(body.Name.ToString().Contains("House"))
		{
			House house = (House)body;
            waterable = true;
			GD.Print("hi");
			// Must be deferred as we can't change physics properties on a physics callback.
        	//GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		}
        else if (body.Name.ToString().Contains("Plant"))
        {
            EmitSignal(SignalName.Grow, waterable);
            waterable = false;
            //GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        }
    }
}