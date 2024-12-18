using Godot;
using System;

public partial class CustomSignals : Node
{
	// Signals
	[Signal] 
	public delegate void ChangeMasterVolumeEventHandler(float value);
	[Signal] 
	public delegate void ChangeMusicVolumeEventHandler(float value);
	[Signal] 
	public delegate void ChangeSFXVolumeEventHandler(float value);

	// Level Loading
	[Signal] public delegate void LoadLevelEventHandler(int index);

	// Methods

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
