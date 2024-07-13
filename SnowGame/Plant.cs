using Godot;
using System;

public partial class Plant : Area2D
{

	[Export]
	public int growth_progress;
	private AnimatedSprite2D anim;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		growth_progress = 0;
		anim.Frame = growth_progress;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		anim.Frame = growth_progress;
	}

	public void OnPlantGrow(bool waterable)
	{
		if(waterable)
		{
			GD.Print("yep");
			growth_progress++;
		}
	}
}
