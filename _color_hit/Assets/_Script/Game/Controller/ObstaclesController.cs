using UnityEngine;
using System.Collections;

public class ObstaclesController : Controller
{
	public override void OnNotification( string alias, Object target, params object[] data )
	{
		switch (alias)
		{
			case N.LineImpactObstacle___:
				{
					if (game.model.lineModel.IsDraw ())
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

