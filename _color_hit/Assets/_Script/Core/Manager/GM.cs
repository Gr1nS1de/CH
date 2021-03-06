﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GM : Controller
{
	public static GM Instance;

	private const string 		LeaderBoardPrivate = "http://dreamlo.com/lb/HmyLFso9EUmOvvnmRzgKsw1og-BQzKSU-1t0Vk36HwIg";

	public bool IsGameStared = false;
	public GameState GameState { get; private set;}
	public StyleId 				CurrentStyleId = StyleId.OrangeWhite;
	[HideInInspector]
	public Vector2 				ScreenSize;
	public bool 				IsLoggsEnabled = true;

	private AnalyticsManager 	_analyticsManager = null;

	void Awake()
	{
		IsGameStared = true;

		if (Instance == null)
		{
			Instance = this;

			InitGameSettings ();
			DOTween.Init ();
			Localization.InitLanguage ();
			Debug.unityLogger.logEnabled = IsLoggsEnabled;
		}
		else
		{
			if (Instance != this)
				Destroy (this.gameObject);
		}
			
	}

	private void InitGameSettings()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		float screenHeight = Camera.main.orthographicSize * 2.0f;
		float screenWidth = screenHeight * Camera.main.aspect;

		ScreenSize = new Vector2 (screenWidth, screenHeight);

		Debug.LogFormat ("ScrrenSize: {0}", ScreenSize);

		if(_analyticsManager == null)
			_analyticsManager = new AnalyticsManager ();
	}

	void Start()
	{		
		GameState = GameState.MainMenu;
		SetStyle(CurrentStyleId, true);
	}
		
	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.GameOver:
				{
					/*
					GameOverData gameOverData = (GameOverData)data[0];

					_gameOversCounter++;

					if (_gameOversCounter > 10)
					{
						_gameOversCounter = 0;
						Resources.UnloadUnusedAssets ();
					}*/
					break;
				}

			case N.SelectStyle_:
				{
					StyleId styleId = (StyleId)data [0];

					if (CurrentStyleId != styleId)
					{
						SetStyle (styleId, false);
					}
					break;
				}

			case N.SelectLevel__:
				{
					int level = (int)data [0];
					int step = (int)data [1];
					LevelView levelView = game.view.GetLevelView (level);

					GoToState (GameState.Play);

					game.model.levelModel.InitCurrentLevel (levelView, step);

					Notify (N.StartLevel__, NotifyType.ALL, levelView, step);
					break;
				}

			case N.FinishStep_:
				{
					int currentStep = (int)data [0];
					LevelView currentLevelView = game.model.levelModel.CurrentLevel;
					StyleData currentStyleData = game.m_Core.styleModel.GetCurrentStyleData ();
					LevelView levelView =currentStep + 1 >= currentStyleData.stepsCount ? game.view.GetLevelView (currentLevelView.LevelIndex + 1) : currentLevelView;
					int step = currentStep + 1 >= currentStyleData.stepsCount ? 0 : currentStep + 1;

					Debug.LogFormat ("FinishStep. level: {0}. step: {1}", levelView.LevelIndex + 1, step);

					Notify (N.StartLevel__, NotifyType.ALL, levelView, step);

					break;
				}

			case N.BackAction:
				{
					switch(GameState)
					{
						case GameState.Play:
							{
								GoToState (GameState.MainMenu);
								break;
							}
					}
					break;
				}

			case N.RetryLevel:
				{
					GoToState (GameState.Play);



					Notify (N.StartLevel__, NotifyType.ALL, game.model.levelModel.CurrentLevel, game.model.levelModel.CurrentStep);
					break;
				}
		}

	}

	private void GoToState(GameState state)
	{
		switch (state)
		{
			case GameState.MainMenu:
				{
					ui.controller.MainContextController.MainContext.IsShowMainMenu = true;
					break;
				}

			case GameState.Play:
				{
					ui.controller.MainContextController.MainContext.IsShowMainMenu = false;
					break;
				}
		}

		GameState = state;
	}

	private void SetStyle(StyleId styleId, bool isInit)
	{
		StyleData styleData = core.styleModel.GetStyleData(styleId);


		CurrentStyleId = styleId;

		Notify (N.SetStyle__, NotifyType.ALL, styleData, isInit);
	}

	/*
	public Gradient		backgroundMenuGradient 	{ get { return _backgroundMenuGradient; }		set { _backgroundMenuGradient = value; } } 
	public Gradient		backgroundGameGradient 	{ get { return _backgroundGameGradient; } }
	public float		menuGradientDuration	{ get { return _menuGradientDuration; } }
	public float		gameGradientDuration	{ get { return _gameGradientDuration; } }
	public Color		currentBackgroundColor 	{ get { return _currentBackgroundColor; }		set { _currentBackgroundColor = value; } } 

	[SerializeField]
	private Gradient	_backgroundMenuGradient;
	[SerializeField]
	private Gradient 	_backgroundGameGradient;
	[SerializeField]
	private Color		_currentBackgroundColor;
	[SerializeField]
	private float		_menuGradientDuration;
	[SerializeField]
	private float		_gameGradientDuration;

	public Sprite[]		PlayerSprites;

	private	GameState	gameState				{ get { return game.model.gameState; } }
	private GameState	_lastGameState;
	private bool 		_fadeColorFlag = false;
	private float 		_fadeColorTimestamp = 0f;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			if (instance != this)
				Destroy (this.gameObject);
		}
	}
	
	void Update()
	{
		if (!game)
			return;
		//cam.backgroundColor = gameManager.m_ThemeDynamicColor;

		switch(gameState)
		{
			case GameState.MAIN_MENU:
				{
					currentBackgroundColor  = EvaluateColorFromGradient(backgroundMenuGradient, menuGradientDuration);
					break;
				}

			case GameState.PLAYING:
				{
					if (_lastGameState != gameState)
						_fadeColorFlag = true;
					
					currentBackgroundColor = EvaluateColorFromGradient(backgroundGameGradient, gameGradientDuration);

					break;
				}
		}

		_lastGameState = gameState;
	}

	private Color EvaluateColorFromGradient(Gradient gradient, float durationTime)
	{
		Color currentColor;

		float t = Mathf.PingPong (Time.time / durationTime, 1f);

		currentColor = gradient.Evaluate (t);

		if (_fadeColorFlag)
		{
			_fadeColorTimestamp = Time.time + 1f;
			_fadeColorFlag = false;
		}

		if(_fadeColorTimestamp > Time.time)
			currentColor = Color.Lerp (currentBackgroundColor, currentColor, Time.deltaTime * 5f);

		return currentColor;
	}*/
}