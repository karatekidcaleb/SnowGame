using Godot;
using System;

public partial class House : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		anim.Play("default");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
