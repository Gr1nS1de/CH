using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GM : Controller
{
	public static GM Instance;

	private const string 		LeaderBoardPrivate = "http://dreamlo.com/lb/HmyLFso9EUmOvvnmRzgKsw1og-BQzKSU-1t0Vk36HwIg";

	public StyleId 				CurrentStyleId = StyleId.OrangeWhite;
	[HideInInspector]
	public Vector2 				ScreenSize;
	public bool 				IsLoggsEnabled = true;

	private AnalyticsManager 	_analyticsManager = null;

	void Awake()
	{
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
		SetStyle(CurrentStyleId);
	}
		
	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.GameOver_:
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

			case N.SelectStyleInput_:
				{
					StyleId styleId = (StyleId)data [0];

					if (CurrentStyleId != styleId)
					{
						SetStyle (styleId);
					}
					break;
				}
		}

	}

	public void SetStyle(StyleId styleId)
	{
		StyleData styleData = core.styleModel.GetStyleData(styleId);


		CurrentStyleId = styleId;

		Notify (N.SetStyle_, NotifyType.ALL, styleData);
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