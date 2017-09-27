using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : Controller 
{
	private LineModel 	_lineModel	{ get { return game.model.lineModel;}	}
	private LineView 	_lineView	{ get { return game.view.lineView;}	}

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.OnStart:
				{
					
					break;
				}

			case N.OnInput____:
				{
					GameObject selectedGameObject = (GameObject)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					Vector2 deltaPosition = (Vector2)data [2];
					ContinuousGesturePhase gesturePhase = (ContinuousGesturePhase) data[3];

					switch(gesturePhase)
					{
						case ContinuousGesturePhase.Started:
							{
								_lineView.StartDraw();
								_lineModel.StartDraw ();
								break;
							}

						case ContinuousGesturePhase.Updated:
							{
								if(deltaPosition.magnitude > 0f)
									_lineView.DrawPoint (currentPosition, true);
								break;
							}

						case ContinuousGesturePhase.Ended:
							{
								_lineView.FinishDraw ();
								_lineModel.FinishDraw ();
								break;
							}
					}
					break;
				}

			case N.LineImpactObstacle___:
				{
					ObstacleModel.ObstacleCollisionType collisionType = (ObstacleModel.ObstacleCollisionType)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					ObstacleView obstacleView = (ObstacleView)data [2];

					switch (collisionType)
					{
						case ObstacleModel.ObstacleCollisionType.Die:
							{
								
								break;
							}

						case ObstacleModel.ObstacleCollisionType.Point:
							{
								
								break;
							}
					}
					break;
				}
		}
	}


}
