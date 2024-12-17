using Godot;
using System;

public partial class UIManager : Node
{
	// Signals
	// Exit Game
	[Signal] public delegate void ExitGameEventHandler();

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
	}

	// Method to reset all UIs to default values
	private void ResetAllUI()
	{
		GetNode<CanvasLayer>("MainMenuUI").Show();
		GetNode<CanvasLayer>("StartMenuUI").Hide();
		GetNode<CanvasLayer>("ControlsHelpUI").Hide();
		GetNode<CanvasLayer>("OptionsUI").Hide();
		GetNode<CanvasLayer>("CreditsUI").Hide();
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

	//*********************************************************************************************************************
	// Battle UI
}
