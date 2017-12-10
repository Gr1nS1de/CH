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
			case N.StartLevel__:
				{
					LevelView levelView =  (LevelView)data [0];
					int step = (int)data [1];
					LineRenderer currentLineRenderer = levelView.StepsList [step].GetComponentInChildren<LineRenderer> (true);

					_lineView.Init (currentLineRenderer);

					ResetLine();
					break;
				}

			case N.RetryLevel:
				{
					ResetLine();
					
					break;
				}

			case N.FinishStep_:
				{
					ResetLine();
					break;
				}

			case N.GameOver:
				{
					ResetLine();
					break;
				}

			case N.DragInput____:
				{
					if (GM.Instance.GameState != GameState.Play)
					{
						return;
					}

					GameObject selectedGameObject = (GameObject)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					Vector2 deltaPosition = (Vector2)data [2];
					ContinuousGesturePhase gesturePhase = (ContinuousGesturePhase) data[3];

					switch(gesturePhase)
					{
						case ContinuousGesturePhase.Started:
							{
								StartDraw();
								break;
							}

						case ContinuousGesturePhase.Updated:
							{
								if (_lineModel.currentState == LineState.Finished)
									return;
								
								if (deltaPosition.magnitude > 0f)
								{
									if (_lineModel.currentState == LineState.Init)
									{
										StartDraw ();
									}

									_lineView.DrawPoint (currentPosition);
									_lineModel.DrawPoint ();
								}
								break;
							}

						case ContinuousGesturePhase.Ended:
							{
								_lineModel.FinishDraw ();
								_lineView.FinishDraw ();
								break;
							}
					}
					break;
				}

			case N.CollisionObstacle___:
				{
					ObstacleModel.ObstacleCollisionType collisionType = (ObstacleModel.ObstacleCollisionType)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					ObstacleView obstacleView = (ObstacleView)data [2];

					switch (collisionType)
					{
						case ObstacleModel.ObstacleCollisionType.Die:
							{
								if (_lineModel.isDrawing)
								{
									_lineView.CollapseLineToStart ();
								}
								else
								{
									_lineView.CollapseLineToEnd ();
								}
								break;
							}

						case ObstacleModel.ObstacleCollisionType.Point:
							{
								if (_lineModel.isDrawing)
								{
									_lineModel.FinishDraw ();
									_lineView.FinishDraw ();
								}
								else
								{
									_lineView.PunchExpandLine ();
								}
								break;
							}
					}

					break;
				}
		}
	}
		
	private void ResetLine()
	{
		_lineModel.InitLine ();
		_lineView.ResetLine ();
	}

	private void StartDraw()
	{
		_lineView.StartDraw();
		_lineModel.StartDraw ();
	}
}
