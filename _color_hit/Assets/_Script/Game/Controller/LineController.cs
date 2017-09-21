using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : Controller 
{
	private LineView _lineView	{ get { return game.view.lineView;}	}

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.OnStart:
				{
					OnStart ();
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
								break;
							}

						case ContinuousGesturePhase.Updated:
							{
								_lineView.DrawPoint (currentPosition, true);
								break;
							}

						case ContinuousGesturePhase.Ended:
							{
								_lineView.FinishDraw ();
								break;
							}
					}
					break;
				}

			case N.LineImpactObstacle__:
				{
					ObstacleModel.ObstacleType obstacleType = (ObstacleModel.ObstacleType)data [0];
					Vector3 currentPosition = (Vector3)data [1];

					break;
				}
		}
	}

	private void OnStart()
	{
		
	}


}
