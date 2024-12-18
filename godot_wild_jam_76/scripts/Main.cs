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

		// Reset all Game Variables
		_gameData.ResetAllGameVariables();

		// Initialize Level Names
		_gameData.InitializeLevelNames();

		// Initialize Level Paths
		_gameData.InitializeLevelPaths();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Handle Exit Game
	private void OnExitGame()
	{
		GD.Print("Exiting the Game!");
		GetTree().Quit();
	}

	// Handle Start Game
	private void OnStartGame(string startingChoice)
	{
		string message = "Starting Game With: " + startingChoice;
		GD.Print(message);

		//int index = _gameData.CurrentLevelIndex;
		//GD.Print(index);
		//int index = _gameData.CurrentLevelIndex + 1;
		int index = 1;
		_customSignals.EmitSignal(nameof(CustomSignals.LoadLevel), index);
		_gameData.IsGameInProgress = true;
		
	}

	// Handle Abandon Game
	private void OnAbandonGame()
	{
		int index = 0;
		_customSignals.EmitSignal(nameof(CustomSignals.LoadLevel), index);

		//_gameData.ResetAllGameVariables();
		_gameData.IsGamePaused = false;
		_gameData.IsGamePausable = false;
		_gameData.IsGameInProgress = false;
	}
}
