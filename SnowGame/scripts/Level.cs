using Godot;
using System;

public partial class Level : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var tree_spawner = GD.Load<PackedScene>("res://scenes/Treething.tscn");
		for (int i=0; i< new Random().Next()%4 + 5.0f; i++)
		{
			AnimatedSprite2D instance = (AnimatedSprite2D)tree_spawner.Instantiate();
			float random_size = GD.Randf() * 0.2f + 0.15f;
			instance.Scale = new Vector2(
				x: random_size,
				y: random_size
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
		for(int i=0; i<GetChildCount() - 1; i++)
		{
			GetChild<Node2D>(i).ZIndex = (int)GetChild<Node2D>(i).Position.Y;
		}
		if(GetNode<House>("House").GetNode<AnimatedSprite2D>("AnimatedSprite2D").Frame == 1)
			GetNode<PointLight2D>("DoorLight").Enabled = false;
		else
			GetNode<PointLight2D>("DoorLight").Enabled = true;
	}

	public void OnChangeLevelRequest()
	{
		GD.Print("hi");
		if(GetNode<StaticBody2D>("Plant").GetNode<AnimatedSprite2D>("AnimatedSprite2D").Frame == 3)
		{
			Input.ActionPress("change_level");
		}
	}
}
