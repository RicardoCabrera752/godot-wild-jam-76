using Godot;
using System;
using System.Collections.Generic;

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
	public bool IsLoadingLevel { get; set; } = false;
	public bool IsLoadingDone { get; set; } = false;
	public float LevelLoadingProgress { get; set; } = 0.0f;

	// Index of current level that is loaded
	public int CurrentLevelIndex { get; set; } = 0;
	// List of Level Names
	public List<string> LevelNames { get; set; } = new List<string>();
	// Dictionary of Level Paths: pair (nodePath, path)
	public Dictionary<string, Tuple<string, string>> LevelPaths { get; set; } = new Dictionary<string, Tuple<string, string>>();

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
		IsGamePaused = false;
		IsGamePausable  = false;
		IsGameInProgress  = false;
		IsGameWon  = false;
		IsGameLost  = false;

		// Audio Metadata

		// World Metadata
		IsMainMenuWorldDead = false;
		IsGameMapWorldDead = false;
		IsBattleMapWorldDead = false;
		IsLoadingLevel = false;
		IsLoadingDone = false;
		LevelLoadingProgress = 0.0f;
		CurrentLevelIndex = 0;

		// Dialogue Metadata

		// Player Metadata
		HellFrostLevel = 0;
		CurrentMission = 0;

		// Battle Metadata
		IsBattleWon = false;
		IsBattleLost = false;
	}

	// Method to Initialize the Level Names
	public void InitializeLevelNames()
	{
		LevelNames.Add("Main Menu");
		LevelNames.Add("Test");
		LevelNames.Add("Test2");
		//LevelPathNames.Add("The Trenches");
		//LevelPathNames.Add("The Wall");
		//LevelPathNames.Add("The Interior");
		//LevelPathNames.Add("The Fotress");
		//LevelPathNames.Add("The Gate");
	}

	// Method to Initialize the Level Paths
	public void InitializeLevelPaths()
	{
		LevelPaths.Add("Main Menu", Tuple.Create("MainMenuLevel", "res://scenes/main_menu_level.tscn"));
		LevelPaths.Add("Test", Tuple.Create("TestLevel", "res://scenes/test_level.tscn"));
		LevelPaths.Add("Test2", Tuple.Create("TestLevel2", "res://scenes/test_level_2.tscn"));
	}

	public override void _Ready()
    {
        Instance = this;
		
    }
}
