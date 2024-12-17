using Godot;
using System;

public partial class Main : Node
{
	// Signals

	// Exports

	// Properties
	// Access to GameData
	private GameData _gameData;
	
	// Access to CustomSignals
	private CustomSignals _customSignals;

	// Methods

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get GameData and CustomSignals
		_gameData = GetTree().Root.GetNode<GameData>("GameData");
		_customSignals = GetTree().Root.GetNode<CustomSignals>("CustomSignals");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Handle Game Exit
	private void OnExitGame()
	{
		GD.Print("Exiting the Game!");
		GetTree().Quit();
	}
}
