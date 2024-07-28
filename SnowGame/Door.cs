using Godot;
using System;

public partial class Door : Godot.AnimatableBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnPlayerEnter()
	{
		AnimatedSprite2D anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		anim.Play("default");
	}
}
