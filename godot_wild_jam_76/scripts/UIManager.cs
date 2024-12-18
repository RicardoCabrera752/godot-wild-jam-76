using Godot;
using System;

public partial class UIManager : Node
{
	// Signals
	// Exit Game
	[Signal] 
	public delegate void ExitGameEventHandler();
	// Start Game
	[Signal] 
	public delegate void StartGameEventHandler(string startingChoice);
	// Abandon Game
	[Signal] 
	public delegate void AbandonGameEventHandler();

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

		// Reset all UIs
		ResetAllUI();

		// Initialize the Volume Slider Values
		GetNode<HSlider>("OptionsUI/VolumeSlidersControl/VolumeSliders/MasterVolumeSlider").Value = _gameData.MasterVolume;
		GetNode<HSlider>("OptionsUI/VolumeSlidersControl/VolumeSliders/MusicVolumeSlider").Value = _gameData.MusicVolume;
		GetNode<HSlider>("OptionsUI/VolumeSlidersControl/VolumeSliders/SFXVolumeSlider").Value = _gameData.SFXVolume;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Check if the Level has been loaded
		if(_gameData.IsLoadingDone)
		{
			//GetNode<CanvasLayer>("LoadingScreen").Hide();
			GetNode<Label>("LoadingScreen/PreseAnyKeyLabel").Show();

			if(Input.IsAnythingPressed())
			{
				GetNode<CanvasLayer>("LoadingScreen").Hide();
				GD.Print("GLHF");

				_gameData.IsLoadingDone = false;
				_gameData.IsLoadingLevel = false;
				_gameData.IsGamePausable = true;
			}
		}

		// Check Player Inputs
		// Check if the Player has paused the game
		if(Input.IsActionJustPressed("pause_game"))
		{
			if(_gameData.IsGameInProgress && _gameData.IsGamePausable && !_gameData.IsGamePaused && !_gameData.IsGameWon && !_gameData.IsGameLost)
			{
				GetTree().Paused = true;
				GD.Print("Game Paused!");

				GetNode<CanvasLayer>("PauseUI").Show();
				_gameData.IsGamePaused = true;

				GetNode<CanvasLayer>("ControlsHelpUI").Hide();
				GetNode<CanvasLayer>("OptionsUI").Hide();
				GetNode<Control>("PauseUI/AbandonGame").Hide();
			}
			// Unpause the Game
			else if (_gameData.IsGameInProgress && _gameData.IsGamePausable && _gameData.IsGamePaused && !_gameData.IsGameWon && !_gameData.IsGameLost)
			{
				GetTree().Paused = false;
				GD.Print("Resuming Game!");

				GetNode<CanvasLayer>("PauseUI").Hide();
				_gameData.IsGamePaused = false;

				GetNode<CanvasLayer>("ControlsHelpUI").Hide();
				GetNode<CanvasLayer>("OptionsUI").Hide();
				GetNode<Control>("PauseUI/AbandonGame").Hide();
			}
		}
	}

	// Method to reset all UIs to default values
	private void ResetAllUI()
	{
		GetNode<CanvasLayer>("MainMenuUI").Show();
		GetNode<CanvasLayer>("StartMenuUI").Hide();
		GetNode<CanvasLayer>("ControlsHelpUI").Hide();
		GetNode<CanvasLayer>("OptionsUI").Hide();
		GetNode<CanvasLayer>("CreditsUI").Hide();
		GetNode<CanvasLayer>("PauseUI").Hide();
		GetNode<CanvasLayer>("LoadingScreen").Hide();
	}

	// Method to show the Loading Screen
	private void ShowLoadingScreen()
	{
		string levelName = _gameData.LevelNames[_gameData.CurrentLevelIndex];
		GetNode<Label>("LoadingScreen/LoadingScreenInformation/VBoxContainer/LevelNameLabel").Text = "Level Name: " + levelName;
		GetNode<CanvasLayer>("LoadingScreen").Show();
	}

	//*********************************************************************************************************************
	// Main Menu

	// Handle Start Game Button being pressed
	private void OnStartGameButtonPressed()
	{
		GD.Print("Start Game Pressed");

		GetNode<CanvasLayer>("MainMenuUI").Hide();
		GetNode<CanvasLayer>("StartMenuUI").Show();
	}

	// Handle Controls/Help Button being pressed
	private void OnControlsHelpButtonPressed()
	{
		GD.Print("Controls/Help Pressed");

		GetNode<CanvasLayer>("MainMenuUI").Hide();
		GetNode<CanvasLayer>("ControlsHelpUI").Show();
	}

	// Handle Options Button being pressed
	private void OnOptionsButtonPressed()
	{
		GD.Print("Options Pressed");

		GetNode<CanvasLayer>("MainMenuUI").Hide();
		GetNode<CanvasLayer>("OptionsUI").Show();
	}

	// Handle Credits Button being pressed
	private void OnCreditsButtonPressed()
	{
		GD.Print("Credits Pressed");

		GetNode<CanvasLayer>("MainMenuUI").Hide();
		GetNode<CanvasLayer>("CreditsUI").Show();
	}

	// Handle Exit Game Button being pressed
	private void OnExitGameButtonPressed()
	{
		GD.Print("Exit Game Pressed");
		EmitSignal(SignalName.ExitGame);
	}

	// Handle Return Button being pressed
	private void OnReturnButtonPressed(string currentMenu)
	{
		string message = "Closing " + currentMenu;
		GD.Print(message);

		GetNode<CanvasLayer>("MainMenuUI").Show();
		GetNode<CanvasLayer>(currentMenu).Hide();
	}

	// Handle Close Button being pressed
	private void OnCloseButtonPressed(string currentMenu)
	{
		string message = "Closing " + currentMenu;
		GD.Print(message);

		if(_gameData.IsGamePaused)
		{
			GetNode<CanvasLayer>(currentMenu).Hide();
		}
		else 
		{
			GetNode<CanvasLayer>("MainMenuUI").Show();
			GetNode<CanvasLayer>(currentMenu).Hide();
		}
	}

	// Volume Sliders:
	// Master Volume Slider
	private void OnMasterVolumeSliderValueChanged(float value)
	{
		_customSignals.EmitSignal(nameof(CustomSignals.ChangeMasterVolume), value);
	}

	// Music Volume Slider
	private void OnMusicVolumeSliderValueChanged(float value)
	{
		_customSignals.EmitSignal(nameof(CustomSignals.ChangeMusicVolume), value);
	}

	// SFX Volume Slider
	private void OnSFXVolumeSliderValueChanged(float value)
	{
		_customSignals.EmitSignal(nameof(CustomSignals.ChangeSFXVolume), value);
	}

	//*********************************************************************************************************************
	// Pause Menu
	// Handle Resume Button being pressed
	private void OnResumeButtonPressed()
	{
		GetTree().Paused = false;
		GD.Print("Resuming Game!");

		GetNode<CanvasLayer>("PauseUI").Hide();
		_gameData.IsGamePaused = false;

		GetNode<CanvasLayer>("ControlsHelpUI").Hide();
		GetNode<CanvasLayer>("OptionsUI").Hide();
		GetNode<Control>("PauseUI/AbandonGame").Hide();
	}

	// Handle Controls/Help Button being pressed

	// Handle Options Button being pressed

	// Handle Abandon Game Button being pressed
	private void OnAbandonGameButtonPressed()
	{
		GD.Print("Abandon Game Pressed");
		GetNode<Control>("PauseUI/AbandonGame").Show();
	}

	// Handle Confirm Abandon Game Button being pressed
	private void OnConfirmAbandonGameButtonPressed()
	{
		GD.Print("Confirm Abandon Game");

		ResetAllUI();
		//_gameData.IsGamePaused = false;
		// Important!!! need to unpause game
		GetTree().Paused = false;

		EmitSignal(SignalName.AbandonGame);
	}

	// Handle Cancel Abandon Game Button being pressed
	private void OnCancelAbandonGameButtonPressed()
	{
		GD.Print("Cancel Abandon Game");
		GetNode<Control>("PauseUI/AbandonGame").Hide();
	}

	//*********************************************************************************************************************
	// Start Game Menu
	// Handle Select Button being pressed
	private void OnSelectButtonPressed(string startingChoice)
	{
		string message;

		// increment current level index
		//_gameData.CurrentLevelIndex++;

		if(startingChoice == "Jumbo")
		{
			message = "Selected Tank: " + startingChoice;
			GD.Print(message);

			EmitSignal(SignalName.StartGame, startingChoice);
		}

		GetNode<CanvasLayer>("StartMenuUI").Hide();
		ShowLoadingScreen();
	}

	//*********************************************************************************************************************
	// Battle UI
}
