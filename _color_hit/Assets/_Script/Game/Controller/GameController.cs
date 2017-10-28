using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : Controller
{
	public LineController				lineController				{ get { return _lineController			= SearchLocal(			_lineController,				typeof(LineController).Name );}}

	private LineController				_lineController;

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
	private GameView 				_gameView	{ get { return game.view;}}
	private PlayerData 		_playerDataModel	{ get { return core.playerData;}}

	private bool _isWaitGameOver = false;
	private Tween _gameOverDelayTween = null;

	public override void OnNotification( string alias, Object target, params object[] data )
	{
		switch ( alias )
		{

			case N.DragInput____:
				{
					
					if (_isWaitGameOver)
					{
						if (_gameOverDelayTween != null)
							_gameOverDelayTween.Kill ();

						Notify (N.GameOver);
					}
					break;
				}

			case N.CollisionObstacle___:
				{
					ObstacleModel.ObstacleCollisionType collisionType = (ObstacleModel.ObstacleCollisionType)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					ObstacleView obstacleView = (ObstacleView)data [2];
					LevelView currentLevel = game.model.levelModel.CurrentLevel;

					Debug.LogFormat ("Line collision {0} at {1}", collisionType, currentPosition);

					switch (collisionType)
					{
						case ObstacleModel.ObstacleCollisionType.Die:
							{
								GameOver ();
								break;
							}
					}
					break;
				}

			case N.ImpactObstacle___:
				{
					ObstacleModel.ObstacleCollisionType collisionType = (ObstacleModel.ObstacleCollisionType)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					ObstacleView obstacleView = (ObstacleView)data [2];
					LevelView currentLevel = game.model.levelModel.CurrentLevel;

					Debug.LogFormat ("Line impact {0} at {1}", collisionType, currentPosition);

					switch (collisionType)
					{
						case ObstacleModel.ObstacleCollisionType.Point:
							{
								Debug.LogFormat ("IsActive obstacle: {0}", game.model.obstacleModel.isActivePointObstacle);
								if (!game.model.obstacleModel.isActivePointObstacle)
								{
									int currentStep = game.model.levelModel.CurrentStep;

									game.model.levelModel.CurrentStep = game.model.levelModel.CurrentStep + 1 >= core.styleModel.GetCurrentStyleData ().stepsCount ? 0 : game.model.levelModel.CurrentStep + 1;

									DOVirtual.DelayedCall(1f, ()=>
									{
										Debug.LogFormat("Notify finish step");
										Notify (N.FinishStep_, NotifyType.GAME, currentStep);
									});
								}
								break;
							}
					}
					break;
				}

			case N.FinishDrawDuplicateLine:
				{
					Debug.LogFormat ("Finish draw duplicate line: {0}", game.model.obstacleModel.isActivePointObstacle);
					if (game.model.obstacleModel.isActivePointObstacle)
					{
						GameOver ();
					}
					break;
				}

			case N.RetryLevel:
				{
					GameOver ();
					break;
				}
					
		}
	}

		
	void Start()
	{

	}
		
	private void GameOver()
	{
		if (_gameOverDelayTween != null)
			_gameOverDelayTween.Kill ();
		
		float delaySecs = game.model.lineModel.lineDrawTimeLength;
		Debug.LogFormat ("GameOver. delay: {0}",delaySecs);
		_isWaitGameOver = true;

		_gameOverDelayTween = DOVirtual.DelayedCall (delaySecs, () =>
		{
			_isWaitGameOver = false;
			Notify (N.GameOver);
		})
			.OnKill (() =>
		{
				_isWaitGameOver = false;
		});

	}


}
