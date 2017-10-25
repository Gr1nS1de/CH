using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleModel : Model
{
	public enum ObstacleCollisionType
	{
		Die,
		Point
	}

	public enum WhiteOrangeObstacleType
	{
		HalfPoint,
		Point,
		Die
	}

	public bool isActivePointObstacle {get { return _obstaclesList.Find(obstacle=>obstacle.CollisionType == ObstacleCollisionType.Point && !obstacle.IsTriggered);}}

	public List<ObstacleView> obstaclesList { get { return _obstaclesList;} }

	private List<ObstacleView> _obstaclesList = new List<ObstacleView>();

	public void InitObstaclesList(LevelView levelView, int step)
	{
		_obstaclesList.Clear ();
		_obstaclesList.AddRange (levelView.StepsList[step].GetComponentsInChildren<ObstacleView> (true));

		if (_obstaclesList.Count == 0)
		{
			Debug.LogErrorFormat ("Error. No obstacles for level: {0}. step: {1}", levelView.LevelIndex + 1, step);
		}
	}

}