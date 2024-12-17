using Godot;
using System;

public partial class AudioManager : Node
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

		// Master Volume Changes
		_customSignals.ChangeMasterVolume += HandleChangeMasterVolume;

		// Music Volume Changes
		_customSignals.ChangeMusicVolume += HandleChangeMusicVolume;

		// SFX Volume Changes
		_customSignals.ChangeSFXVolume += HandleChangeSFXVolume;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Handle Change Master Volume Signal
	private void HandleChangeMasterVolume(float value)
	{
		int index = _gameData.MasterVolumeIndex;
		AudioServer.SetBusVolumeDb(index, Mathf.LinearToDb(value));
		_gameData.MasterVolume = value;
	}

	// Handle Change Music Volume Signal
	private void HandleChangeMusicVolume(float value)
	{
		int index = _gameData.MusicVolumeIndex;
		AudioServer.SetBusVolumeDb(index, Mathf.LinearToDb(value));
		_gameData.MusicVolume = value;
	}

	// Handle Change SFX Volume Signal
	private void HandleChangeSFXVolume(float value)
	{
		int index = _gameData.SFXVolumeIndex;
		AudioServer.SetBusVolumeDb(index, Mathf.LinearToDb(value));
		_gameData.SFXVolume = value;
	}
}
