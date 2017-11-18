using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using DG.Tweening;

public enum GameState
{
	MainMenu,
	Play,
	Pause,
	GameOver
}

public enum GameSpeedState
{
	NotDefined,
	Speed_1,
	Speed_2,
	Speed_3,
	Speed_4,
	Speed_5,
	Speed_6,
	Speed_7
}

public enum GameType
{
	Classic
}

/*

public class GameOverData
{
	public GameOverData(){}
	public GameOverData(GameOverData gameOverData)
	{
		GameType = gameOverData.GameType;
		CoinsCount = gameOverData.CoinsCount;
		CrystalsCount = gameOverData.CrystalsCount;
		MagnetsCount = gameOverData.MagnetsCount;
		ScoreCount = gameOverData.ScoreCount;
		IsNewRecord = gameOverData.IsNewRecord;
	}

	public GameType GameType;
	public int CoinsCount;
	public int CrystalsCount;
	public int MagnetsCount;
	public int ScoreCount;
	public bool IsNewRecord;
}*/

public class GameModel : Model
{
	public LineModel			lineModel			{ get { return _lineModel 		= SearchLocal(_lineModel, 		typeof(LineModel).Name );}}
	public ObstacleModel		obstacleModel		{ get { return _obstacleModel 	= SearchLocal(_obstacleModel, 	typeof(ObstacleModel).Name );}}
	public LevelModel			levelModel			{ get { return _levelModel 		= SearchLocal(_levelModel, 		typeof(LevelModel).Name );}}

	private LevelModel			_levelModel;
	private ObstacleModel		_obstacleModel;
	private LineModel			_lineModel;

	public bool isWaitChangeStep;
	//public RocketModel					rocketModel				{ get { return _rocketModel					= SearchLocal<RocketModel>(					_rocketModel,				typeof(RocketModel).Name );}}

	//private RocketModel			_rocketModel;
}
	