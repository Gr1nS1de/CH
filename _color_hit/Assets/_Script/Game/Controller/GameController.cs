using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : Controller
{
	/*
	#region Declare controllers reference
	//public CameraController					cameraController				{ get { return _cameraController 			= SearchLocal<CameraController>(			_cameraController,				typeof(CameraController).Name ); } }
	public GameSoundController				gameSoundController				{ get { return _gameSoundController			= SearchLocal<GameSoundController>(			_gameSoundController,			typeof(GameSoundController).Name ); } }
	public GameResourcesController			resourcesController				{ get { return _resourcesController 		= SearchLocal<GameResourcesController>(		_resourcesController,			typeof(GameResourcesController).Name ); } }
	public ObjectsPoolController			objectsPoolController			{ get { return _objectsPoolController 		= SearchLocal<ObjectsPoolController> (		_objectsPoolController, 		typeof(ObjectsPoolController).Name);}}
	public PlayerController 				playerController				{ get { return _playerController			= SearchLocal<PlayerController>(			_playerController,				typeof(PlayerController).Name );}}
	public ItemsFactoryController			itemsFactoryController			{ get { return _itemsFactoryController		= SearchLocal<ItemsFactoryController>(		_itemsFactoryController,		typeof(ItemsFactoryController).Name );}}
	public BonusesFactoryController			bonusesFactoryController		{ get { return _bonusesFactoryController	= SearchLocal<BonusesFactoryController>(	_bonusesFactoryController,		typeof(BonusesFactoryController).Name );}}
	public PlatformsFactoryController		platformsFactoryController		{ get { return _platformsFactoryController	= SearchLocal<PlatformsFactoryController>(	_platformsFactoryController,	typeof(PlatformsFactoryController).Name );}}
	public DistructibleController			distructibleController			{ get { return _distructibleController		= SearchLocal<DistructibleController>(		_distructibleController,		typeof(DistructibleController).Name );}}
	public GameSpeedController				gameSpeedController				{ get { return _gameSpeedController			= SearchLocal<GameSpeedController>(			_gameSpeedController,			typeof(GameSpeedController).Name );}}
	public RocketsFactoryController			rocketsFactoryController		{ get { return _rocketsFactoryController	= SearchLocal<RocketsFactoryController>(	_rocketsFactoryController,		typeof(RocketsFactoryController).Name );}}
	public RocketsController				rocketsController				{ get { return _rocketsController			= SearchLocal<RocketsController>(			_rocketsController,				typeof(RocketsController).Name );}}

	private RocketsController				_rocketsController;
	private RocketsFactoryController		_rocketsFactoryController;
	private GameSpeedController				_gameSpeedController;
	private DistructibleController			_distructibleController;
	private PlatformsFactoryController		_platformsFactoryController;
	private BonusesFactoryController		_bonusesFactoryController;
	private ItemsFactoryController			_itemsFactoryController;
	private PlayerController				_playerController;
	//private CameraController				_cameraController;
	private GameSoundController				_gameSoundController;
	private GameResourcesController			_resourcesController;
	private ObjectsPoolController 			_objectsPoolController;
	#endregion
*/
	private GameModel 				_gameModel	{ get { return game.model;}}
	private PlayerDataModel 		_playerDataModel	{ get { return core.playerDataModel;}}

	public override void OnNotification( string alias, Object target, params object[] data )
	{
		switch ( alias )
		{
			case N.RCAwakeLoad:
				{
					break;
				}

			case N.OnStart:
				{
					OnStart();

					SetGameState( GameState.MainMenu );
					break;
				}

			case N.GameStart:
				{
					OnGameStartPlay ();

					break;
				}

			case N.GamePlay:
				{
					SetGameState (GameState.Playing);
					break;
				}
					

			case N.OnPlayerInvisible:
				{
					GameOver ();
					break;
				}

			case N.GamePause:
				{
					SetGameState(GameState.Pause);
					break;
				}

			case N.GameAddScore:
				{
					//_gameModel.gameOverData.ScoreCount++;
					break;
				}

			case N.GameContinue:
				{
					SetGameState( GameState.Playing);
					break;
				}
					
		}
	}

	#region public methods
	public void SetGameTheme()//GameTheme gameTheme)
	{
		//_gameModel.gameTheme = gameTheme;

		//Notify (N.GameThemeChanged_, NotifyType.GAME, gameTheme);
	}
	#endregion

	private void OnStart()
	{

	}

	private void OnGameStartPlay()
	{

		//m_PointText.text = _pointScore.ToString();
	}

	private void SetGameState(GameState gameState)
	{
		Debug.LogFormat ("GameState = {0}", gameState); 

		//_gameModel.gameState = gameState;
	}

		

	private void GameOver()
	{

		SetGameState( GameState.GameOver);

		Notify (N.GameOver_, NotifyType.ALL);//, new GameOverData (_gameModel.gameOverData));

		//ReportScoreToLeaderboard(point);

		//_player.DesactivateTouchControl();

		//DOTween.KillAll();
		//StopAllCoroutines();

		//Utils.SetLastScore(_gameModel.currentScore);

		//playerModel.particleTrace.Stop ();

		//ShowAds();

		//_soundManager.PlayFail();

		//ui.controller.OnGameOver(() =>
		//{
			//ReloadScene();
		//});
	}
		

	private void ReloadScene()
	{

		#if UNITY_5_3_OR_NEWER
		SceneManager.LoadSceneAsync( 0, LoadSceneMode.Single );
		#else
		Application.LoadLevel(Application.loadedLevel);
		#endif
	}
}
