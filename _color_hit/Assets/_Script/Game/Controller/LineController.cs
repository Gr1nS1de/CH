using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : Controller 
{
	private LineModel 	_lineModel	{ get { return game.model.lineModel;}	}
	private LineView 	_lineView	{ get { return game.view.lineView;}	}

	private bool _blockLine = false;

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.DragInput____:
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
								if (_blockLine)
									return;
								
								if(deltaPosition.magnitude > 0f)
									_lineView.DrawPoint (currentPosition);
								break;
							}

						case ContinuousGesturePhase.Ended:
							{
								_lineModel.FinishDraw ();

								if (_blockLine)
								{
									_blockLine = false;
									return;
								}

								_lineView.FinishDraw ();
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

					if (_lineModel.isDraw)
					{
						_blockLine = true;
						_lineView.CollapseLineToStart ();
						return;
					}

					switch (collisionType)
					{
						case ObstacleModel.ObstacleCollisionType.Die:
							{
								_lineView.CollapseLineToEnd ();
								break;
							}

						case ObstacleModel.ObstacleCollisionType.Point:
							{
								_lineView.PunchExpandLine ();
								break;
							}
					}
					break;
				}
		}
	}
		
}
