using Godot;
using System;
using System.Runtime.Versioning;

public partial class WorldManager : Node3D
{
	// Signals

	// Exports
	// MainMenuLevel Instance
	[Export] 
	public PackedScene MainMenuLevelScene { get; set; }
	[Export]
	public string MainMenuLevelPath { get; set; } = "res://scenes/main_menu_level.tscn";

	// TestLevel Instance
	[Export] PackedScene TestLevelScene { get; set; }
	[Export] public string TestLevelPath { get; set; } = "res://scenes/test_level.tscn";

	// Properties
	// Access to GameData
	private GameData _gameData;
	
	// Access to CustomSignals
	private CustomSignals _customSignals;

	public MainMenuLevel MainMenuLevelInstance;

	// Generalized Level Variables 
	public string LevelPath;
	public string LevelNodePath;
	public PackedScene LevelScene;
	public Node LevelInstance;

	// Methods

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get GameData and CustomSignals
		_gameData = GetTree().Root.GetNode<GameData>("GameData");
		_customSignals = GetTree().Root.GetNode<CustomSignals>("CustomSignals");

		// Load the Main Menu Level on Game Startup
		//MainMenuLevelScene = ResourceLoader.Load<PackedScene>(MainMenuLevelPath);
		//MainMenuLevelInstance = MainMenuLevelScene.Instantiate<MainMenuLevel>();
		//AddChild(MainMenuLevelInstance);

		//ResourceLoader.LoadThreadedRequest(MainMenuLevelPath, "", false);
		ResourceLoader.LoadThreadedRequest(MainMenuLevelPath, "", false, ResourceLoader.CacheMode.Ignore);
		var levelScene = (PackedScene)ResourceLoader.LoadThreadedGet(MainMenuLevelPath);

		var level = levelScene.Instantiate();

		AddChild(level);

		// Connect
		_customSignals.LoadLevel += HandleLoadLevel;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Check if a Level is being loaded
		if(_gameData.IsLoadingLevel)
		{
			var progress = new Godot.Collections.Array();
			var status = ResourceLoader.LoadThreadedGetStatus(LevelPath, progress);
			GD.Print("I am here");

			// Check if the Level load status is In Progress
			if(status == ResourceLoader.ThreadLoadStatus.InProgress)
			{
				//_gameData.LevelLoadingProgress = (float)progress[0] * 100;
				GD.Print("Progress");
				GD.Print((float)progress[0] * 100);

				GetTree().Root.GetNode<ProgressBar>("Main/UIManager/LoadingScreen/LoadingProgressBar").Value = (float)progress[0] * 100;
			}
			// Else the Level load status is Loaded
			else if(status == ResourceLoader.ThreadLoadStatus.Loaded)
			{
				GD.Print("Loading Done!");

				var levelScene = (PackedScene)ResourceLoader.LoadThreadedGet(LevelPath);

				LevelInstance = levelScene.Instantiate();

				AddChild(LevelInstance);

				_gameData.IsLoadingLevel = false;
				_gameData.IsLoadingDone = true;
				GetTree().Root.GetNode<ProgressBar>("Main/UIManager/LoadingScreen/LoadingProgressBar").Value = (float)progress[0] * 100;
			}
			else if(status == ResourceLoader.ThreadLoadStatus.InvalidResource)
			{
				GD.Print("Invalid!");
				SetProcess(false);
			}
			else if(status == ResourceLoader.ThreadLoadStatus.Failed)
			{
				GD.Print("Failed!");
				SetProcess(false);
			}
		}
	}

	// Load Level 1
	private void LoadMainMenu()
	{
		ResourceLoader.LoadThreadedRequest(MainMenuLevelPath);
		_gameData.IsLoadingLevel = true;
		_gameData.IsLoadingDone = false;
		_gameData.IsGamePausable = false;
	}

	// Handle Load Level Request
	private void HandleLoadLevel(int index)
	{
		// get current level data
		int currentIndex = _gameData.CurrentLevelIndex;
		string currentLevelName = _gameData.LevelNames[currentIndex];
		string currentNodePath = _gameData.LevelPaths[currentLevelName].Item1;

		// get next level data
		int nextIndex = index;
		string nextLevelName = _gameData.LevelNames[nextIndex];
		LevelPath = _gameData.LevelPaths[nextLevelName].Item2;

		// update current index
		_gameData.CurrentLevelIndex = nextIndex;

		//int index = _gameData.CurrentLevelIndex;
		//string levelName = _gameData.LevelNames[index];
		//LevelPath = _gameData.LevelPaths[levelName].Item2;

		//int previousIndex = index - 1;
		//string previousLevelName = _gameData.LevelNames[previousIndex];
		//string nodePath = _gameData.LevelPaths[previousLevelName].Item1;


		// Kill the current level
		GetTree().Root.GetNode<Node3D>("Main/WorldManager/" + currentNodePath).QueueFree();
		string killMessage = "Killed Level: " + currentLevelName;
		GD.Print(killMessage);

		// Load the next level
		/*
		ResourceLoader.LoadThreadedRequest(LevelPath, "", true);
		_gameData.IsLoadingLevel = true;
		_gameData.IsLoadingDone = false;
		_gameData.IsGamePausable = false;

		string loadMessage = "Loading Level: " + nextLevelName;
		GD.Print(loadMessage);
		*/

		// Load the next level
		
		if(ResourceLoader.HasCached(LevelPath))
		{
			//LevelScene = (PackedScene)ResourceLoader.LoadThreadedGet(LevelPath);

			//var levelInstance = LevelScene.Instantiate();

			//AddChild(levelInstance);

			GD.Print("Loading Done! Level was already Cached!");
			_gameData.IsLoadingLevel = true;
			_gameData.IsLoadingDone = false;
			ResourceLoader.LoadThreadedRequest(LevelPath, "", false,ResourceLoader.CacheMode.Replace);

			/*
			var levelScene = (PackedScene)ResourceLoader.LoadThreadedGet(LevelPath);
			var progress = new Godot.Collections.Array();
			var status = ResourceLoader.LoadThreadedGetStatus(LevelPath, progress);

			GD.Print(status);
			GD.Print(levelScene);
			LevelInstance = levelScene.Instantiate();

			AddChild(LevelInstance);

			_gameData.IsLoadingLevel = false;
			_gameData.IsLoadingDone = true;
			GetTree().Root.GetNode<ProgressBar>("Main/UIManager/LoadingScreen/LoadingProgressBar").Value = 100;
			*/
		}
		else 
		{
			GD.Print("Level Path " + LevelPath);
			_gameData.IsLoadingLevel = true;
			_gameData.IsLoadingDone = false;
			_gameData.IsGamePausable = false;
			//ResourceLoader.LoadThreadedRequest(LevelPath, "", false);
			ResourceLoader.LoadThreadedRequest(LevelPath, "", false, ResourceLoader.CacheMode.Ignore);

			string loadMessage = "Loading Level: " + nextLevelName;
			GD.Print(loadMessage);

			//LevelScene = (PackedScene)ResourceLoader.LoadThreadedGet(LevelPath);

			//var levelInstance = LevelScene.Instantiate();

			//AddChild(levelInstance);
		}
		
	}
}
