using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstaclesController : Controller
{
	private LevelView _currentLevelView = null;
	private ObstacleModel _obstacleModel {get { return game.model.obstacleModel;}}

	public override void OnNotification( string alias, Object target, params object[] data )
	{
		switch (alias)
		{
			case N.StartLevel__:
				{
					LevelView levelView =  (LevelView)data [0];
					int step = (int)data [1];

					_obstacleModel.InitObstaclesList (levelView, step);
					break;
				}

			case N.LineImpactObstacle___:
				{
					if (game.model.lineModel.isDraw)
					{
						return;
					}

					ObstacleModel.ObstacleCollisionType collisionType = (ObstacleModel.ObstacleCollisionType)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					ObstacleView obstacleView = (ObstacleView)data [2];

					obstacleView.Collision ();

					break;
				}

		}
	}

}

