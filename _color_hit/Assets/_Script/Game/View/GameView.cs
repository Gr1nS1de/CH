using UnityEngine;
using System.Collections;

public class GameView : View
{
	
	public LineView					lineView			{ get { return _lineView 		= SearchLocal<LineView>(			_lineView,			typeof(LineView).Name ); } }
	//public PlayerView				playerView			{ get { return _playerView 			= SearchLocal<PlayerView>(			_playerView,			typeof(PlayerView).Name ); } }
	//public ObjectsPoolView		objectsPoolView		{ get { return _objectsPoolView		= SearchLocal<ObjectsPoolView>(		_objectsPoolView,		typeof(ObjectsPoolView).Name);}}
	//public BackgroundView			backgroundView		{ get { return _backgroundView		= SearchLocal<BackgroundView>(		_backgroundView,		typeof(BackgroundView).Name);}}

	private LineView				_lineView;
	//private BackgroundView		_backgroundView;
	//private PlayerView			_payerView;
	//private ObjectsPoolView		_objectsPoolView;


}
