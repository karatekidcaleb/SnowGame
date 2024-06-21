using Godot;
using System;

public partial class Main : Node2D
{
	private Level level1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var level_loader = GD.Load<PackedScene>("res://Level.tscn");
		GD.Print("Hello, world!");
		level1 = (Level)level_loader.Instantiate();
		AddChild(level1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("change_level"))
    	{
        	level1.QueueFree();
    	}
	}
}
