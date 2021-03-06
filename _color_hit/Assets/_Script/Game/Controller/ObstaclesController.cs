﻿using UnityEngine;
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

			case N.ImpactObstacle___:
				{
					ObstacleModel.ObstacleCollisionType collisionType = (ObstacleModel.ObstacleCollisionType)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					ObstacleView obstacleView = (ObstacleView)data [2];

					obstacleView.Impact ();

					break;
				}

			case N.CollisionObstacle___:
				{
					ObstacleModel.ObstacleCollisionType collisionType = (ObstacleModel.ObstacleCollisionType)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					ObstacleView obstacleView = (ObstacleView)data [2];
					
					obstacleView.IsTriggered = true;

					if (game.model.lineModel.isDrawing)
					{
						if(collisionType == ObstacleModel.ObstacleCollisionType.Die)
							return;
					}

					Notify (N.ImpactObstacle___, NotifyType.GAME, collisionType, currentPosition, obstacleView);
					break;
				}

			case N.GameOver:
				{
					ResetObstacles ();
					break;
				}

			case N.DragInput____:
				{
					GameObject selectedGameObject = (GameObject)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					Vector2 deltaPosition = (Vector2)data [2];
					ContinuousGesturePhase gesturePhase = (ContinuousGesturePhase) data[3];

					switch (gesturePhase)
					{
						case ContinuousGesturePhase.Started:
							{
								Debug.LogFormat ("Drag start. isWaitChangeStep: {0}", game.model.isWaitChangeStep);

								ResetObstacles ();

								break;
							}
					}
					break;
				}

		}
	}

	private void ResetObstacles()
	{
		game.view.GetCurrentStyleView().transform.parent.gameObject.SetActive(false);
		game.view.GetCurrentStyleView().transform.parent.gameObject.SetActive(true);
	}

}

