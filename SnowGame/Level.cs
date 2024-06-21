using Godot;
using System;

public partial class Level : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var tree_spawner = GD.Load<PackedScene>("res://Treething.tscn");
		for (int i=0; i< new Random().Next()%5; i++)
		{
			AnimatedSprite2D instance = (AnimatedSprite2D)tree_spawner.Instantiate();
			instance.Scale = new Vector2(
				x: GD.Randf() * 3 + 1,
				y: GD.Randf() * 3 + 1
			);
			instance.Position = new Vector2(
				x: GD.Randf() * GetViewportRect().Size.X,
				y: GD.Randf() * GetViewportRect().Size.Y
			);
			AddChild(instance);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
