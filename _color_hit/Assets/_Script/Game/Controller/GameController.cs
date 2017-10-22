using UnityEngine;
using System.Collections;
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
	private GameView 				_gameView	{ get { return game.view;}}
	private PlayerData 		_playerDataModel	{ get { return core.playerData;}}

	public override void OnNotification( string alias, Object target, params object[] data )
	{
		switch ( alias )
		{

			case N.LineImpactObstacle___:
				{
					ObstacleModel.ObstacleCollisionType collisionType = (ObstacleModel.ObstacleCollisionType)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					ObstacleView obstacleView = (ObstacleView)data [2];

					Debug.LogFormat ("Line impact {0} at {1}", collisionType, currentPosition);

					switch (collisionType)
					{
						case ObstacleModel.ObstacleCollisionType.Die:
							{
								Notify (N.GameOver_);
								break;
							}

						case ObstacleModel.ObstacleCollisionType.Point:
							{
								break;
							}
					}
					break;
				}

			case N.GameOver_:
				{
					_gameView.GetCurrentStyleView().transform.parent.gameObject.SetActive(false);
					_gameView.GetCurrentStyleView().transform.parent.gameObject.SetActive(true);

					break;
				}

			case N.RetryLevel:
				{
					_gameView.GetCurrentStyleView().transform.parent.gameObject.SetActive(false);
					_gameView.GetCurrentStyleView().transform.parent.gameObject.SetActive(true);
					break;
				}
					
		}
	}
		
	void Start()
	{

	}
		

}
