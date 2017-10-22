using UnityEngine;
using System.Collections;

public class Core : BaseApplication<GameApplication, MenuApplication>
{
	#region controllers
	public PurchaseController		purchaseController		{ get { return _purchaseController		= SearchLocal(	_purchaseController,	typeof(PurchaseController).Name );}}
	public PlayerDataController		playerDataController	{ get { return _playerDataController	= SearchLocal(	_playerDataController,	typeof(PlayerDataController).Name );}}

	private PlayerDataController	_playerDataController;
	private PurchaseController		_purchaseController;
	#endregion

	#region models
	public PlayerData			playerData;
	public StyleModel			styleModel				{ get { return _styleModel 			= SearchLocal(	_styleModel,		typeof(StyleModel).Name );}}

	private StyleModel			_styleModel;
	#endregion
}


