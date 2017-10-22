using UnityEngine;
using System.Collections;

public class ObstaclesController : Controller
{
	public override void OnNotification( string alias, Object target, params object[] data )
	{
		switch (alias)
		{
			case N.StartLevel__:
				{
					int level = (int)data [0];
					int step = (int)data [1];

					LevelView levelView = game.view.GetCurrentStyleView().LevelsList.Find(lView=>lView.LevelIndex == level);
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

