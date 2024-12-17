using Godot;
using System;

public partial class GameData : Node
{
	public static GameData Instance { get; private set; }

	// Properties
	// General Metadata
	public bool IsGamePaused { get; set; } = false;
	public bool IsGamePausable { get; set; } = false;
	public bool IsGameInProgress { get; set; } = false;
	public bool IsGameWon { get; set; } = false;
	public bool IsGameLost { get; set; } = false;

	// Audio Metadata
	// Audio Bus Indices
	public int MasterVolumeIndex { get; set; } = AudioServer.GetBusIndex("Master");
	public int MusicVolumeIndex { get; set; } = AudioServer.GetBusIndex("Music");
	public int SFXVolumeIndex { get; set; } = AudioServer.GetBusIndex("SFX");
	// Audio Slider Values
	public float MasterVolume { get; set; } = 0.75f;
	public float MusicVolume { get; set; } = 0.0f;
	public float SFXVolume { get; set; } = 0.65f;

	// World Metadata
	public bool IsMainMenuWorldDead { get; set; } = false;
	public bool IsGameMapWorldDead { get; set; } = false;
	public bool IsBattleMapWorldDead { get; set; } = false;

	// Dialogue Metadata

	// Player Metadata
	// Current Hell Frost Level
	public int HellFrostLevel { get; set; } = 0;
	// Current Units
	// Current Support Strikes
	// Current Mission Progress
	public int CurrentMission { get; set; } = 0;

	// Battle Metadata
	public bool IsBattleWon { get; set; } = false;
	public bool IsBattleLost { get; set; } = false;

	// Master Metadata
	// Collection of all Units
	// Collection of all Weapons
	// Collection of all Events

	// Methods
	// Method to Reset all Game Metadata to default values
	public void ResetAllGameVariables()
	{
		// General Metadata

		// Audio Metadata

		// World Metadata

		// Dialogue Metadata

		// Player Metadata

		// Battle Metadata
	}

	public override void _Ready()
    {
        Instance = this;
		
    }
}
