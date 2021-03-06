﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class LineView : View
{
	public EdgeCollider2D LineCollider;

	private List<Vector3> 	_pointsList { get { return _lineModel.pointsList; } }
	//private List<Vector3> 	_spiralPointsList = new List<Vector3> ();
	private List<Vector2> 	_colliderList { get { return _lineModel.colliderList; } }
	private float 			_vertexSmooth = 0.5f;
	private int 			_lastIndex = -1;
	private LineRenderer	_lineRenderer; 

	private IEnumerator 	_duplicateLineRoutine = null;
	private IEnumerator 	_drawPointRoutine = null;
	private Tween 			_collapseLineToStartTween = null;
	private Tween 			_collapseLineToEndTween = null;
	private Sequence 		_punchExpandLineSequence = null;
	private	string			_lineTweenId = "line.tween.id";
	private bool			_isFinishedDuplicate = false;
	private bool			_isFinishedInitLine = false;
	private Sequence 		_initLineSequence = null;
	private int				_pointsCount = 0;

	private LineModel 		_lineModel { get { return game.model.lineModel;}}

	#region public methods
	/// <summary>
	/// Called on start level
	/// </summary>
	public void Init(LineRenderer currentLineRenderer)
	{
		Debug.LogFormat ("Current line renderer: {0}. parent: {1}", currentLineRenderer.transform.parent.name, currentLineRenderer.transform.parent.parent.name);

		//_spiralPointsList.Clear ();


		_lineRenderer = currentLineRenderer;

		_isFinishedInitLine = false;
	}

	public void StartDraw()
	{
		ResetLine ();
		LineCollider.enabled = true;
	}

	public void ResetLine()
	{	
		Debug.LogFormat ("ResetLine");

		_lineRenderer.SetVertexCount(0);

		LineCollider.enabled = false;
		LineCollider.Reset();

		DOTween.Kill (_lineTweenId);

		_isFinishedDuplicate = false;

		if(_duplicateLineRoutine != null)
			StopCoroutine (_duplicateLineRoutine);

		if(_drawPointRoutine != null)
			StopCoroutine (_drawPointRoutine);

		StopAllCoroutines ();
	}

	public void DrawPoint(Vector3 pos, bool isClampLine = false)
	{
		_drawPointRoutine = DrawPointRoutine (pos, isClampLine);

		StartCoroutine (_drawPointRoutine);
	}

	public void FinishDraw()
	{
		Debug.LogFormat("FinishDraw. line points count: {0}", _pointsList.Count);
		_duplicateLineRoutine = ContinueDuplicateLine();
		/*
		if (_isFinishedInitLine)
		{
			_initLineSequence.Kill ();

			_initLineSequence = DOTween.Sequence ();

			_initLineSequence
				.Append (_currentLineRenderer.transform.GetChild (_currentLineRenderer.transform.childCount - 2).DOMove (_pointsList[0], 0.1f).SetEase (Ease.Linear));

			for (int a = 0; a < _currentLineRenderer.transform.childCount - 2; a++)
			{
				_initLineSequence
					.Join (_currentLineRenderer.transform.GetChild (a).DOMove (_pointsList[0], 0.1f).SetEase (Ease.Linear));
			}

			_initLineSequence.OnComplete (() =>
			{
				if (_currentLineRenderer.gameObject.activeSelf)
					_currentLineRenderer.gameObject.SetActive (false);
			});

			_isFinishedInitLine = false;
		}
*/
		DOVirtual.DelayedCall (0.1f, () =>
		{
			StartCoroutine (_duplicateLineRoutine);
		});
	}

	public void PunchExpandLine()
	{
		_punchExpandLineSequence = DOTween.Sequence ();

		_punchExpandLineSequence
			.Append(DOTween.To (() => 0.05f, width =>
				{
					_lineRenderer.startWidth = width;
					_lineRenderer.endWidth = width;
				}, 0.1f, 0.15f)
				.SetEase(Ease.Flash)
			)
			.Append(DOTween.To (() => 0.1f, width =>
				{
					_lineRenderer.startWidth = width;
					_lineRenderer.endWidth = width;
				}, 0.05f, 0.1f)
				.SetEase(Ease.Linear)
			)
			.SetId(_lineTweenId);
	}

	public void CollapseLineToStart()
	{
		Debug.LogFormat ("CollapseLineToStart. line points count: {0}", _pointsList.Count);

		if(_duplicateLineRoutine != null)
			StopCoroutine(_duplicateLineRoutine);

		_collapseLineToStartTween = 
			DOTween.To (() => _lineRenderer.positionCount-1, pointIndex =>
			{
				_lineRenderer.SetVertexCount(pointIndex);

			}, 0, _lineModel.lineDrawTimeLength).SetEase(Ease.Linear)
			.OnStart(()=>
			{
				LineCollider.enabled = false;
			})
			.OnComplete(()=>
			{
				//ClearLinePoints();
			})
			.SetId(_lineTweenId);
	}

	public void CollapseLineToEnd()
	{
		Debug.LogFormat ("CollapseLineToEnd");

		StopCoroutine(_duplicateLineRoutine);

		_lastIndex = -1;
		int linesPointsCount = _lineRenderer.positionCount - 1;

		_collapseLineToEndTween = 
			DOTween.To (() =>linesPointsCount, pointIndex =>
			{
				if(_lastIndex == -1 ||_lastIndex - pointIndex > 0)
				{
					Vector3[] linePositions = new Vector3[_lineRenderer.positionCount];	
					_lineRenderer.GetPositions(linePositions);
					var linePositionsList = linePositions.ToList();
					int removeRange = _lastIndex == -1 ? 1 : _lastIndex - pointIndex;

					linePositionsList.RemoveRange(0, _lastIndex == -1 ? linePositionsList.Count - pointIndex : _lastIndex - pointIndex);

					_lineRenderer.positionCount = linePositionsList.Count;
					_lineRenderer.SetPositions(linePositionsList.ToArray());

					_lastIndex = pointIndex;
				}

			}, 0, _lineModel.lineDrawTimeLength).SetEase(Ease.Linear)
			.OnStart(()=>
			{
				LineCollider.enabled = false;
			})
			.OnKill(()=>
			{
					Debug.LogFormat("Kill collapse line to end");
			})
			.OnComplete(()=>
			{
				//ClearLinePoints();
			})
			.SetId(_lineTweenId);
	}
		
	public override void OnRendererTriggerEnter (ViewTriggerDetect triggerDetector, Collider2D otherCollider)
	{
		if (_pointsList.Count == 0)
			return;

		ObstacleModel.ObstacleCollisionType obstacleCollisionType = Utils.GetObstacleCollisionType(otherCollider.gameObject.layer);
		ObstacleView obstacleView = otherCollider.GetComponentInParent<ObstacleView> ();

		if (obstacleView == null)
		{
			Debug.LogErrorFormat ("LineView. OnRendererTriggerEnter. Error: obstacleView == null!");
			return;
		}

		Notify (N.CollisionObstacle___, NotifyType.GAME, obstacleCollisionType, _pointsList[_pointsList.Count-1], obstacleView);
	}
		
	#endregion

	private IEnumerator DrawPointRoutine(Vector3 pos, bool isClampLine)
	{
		if (!_pointsList.Contains (pos))
		{
			pos.z = 0f;

			_pointsList.Add (pos);
			/*
			if (!_lineModel.isFirstDrawInited || _isFinishedInitLine)
			{
				_isFinishedInitLine = true;

				if (_initLineSequence != null && _initLineSequence.IsActive ())
					_initLineSequence.Kill ();

				_initLineSequence = DOTween.Sequence ();

				if (!_lineModel.isFirstDrawInited)
				{
					_currentLineRenderer.transform.GetChild (_currentLineRenderer.transform.childCount - 1).DOMove (pos, 0.2f).SetEase(Ease.Linear);
				}

				for (int i = 0; i < _pointsList.Count; i++)
				{
					_initLineSequence
						.Append (_currentLineRenderer.transform.GetChild (i).DOMove(_currentLineRenderer.transform.GetChild (i + 1).position, 0.02f).SetEase(Ease.Linear));

					for (int a = 0; a < i; a++)
					{
						_initLineSequence
							.Join (_currentLineRenderer.transform.GetChild (a).DOMove(_currentLineRenderer.transform.GetChild (i + 1).position, 0.02f).SetEase(Ease.Linear));
						
					}
				}

				if (_pointsList.Count >= _currentLineRenderer.transform.childCount - 2)
				{
					_initLineSequence
						.Append (_currentLineRenderer.transform.GetChild (_currentLineRenderer.transform.childCount - 2).DOMove (pos, 0.05f).SetEase (Ease.Linear));

					for (int a = 0; a < _currentLineRenderer.transform.childCount - 2; a++)
					{
						_initLineSequence
						.Join (_currentLineRenderer.transform.GetChild (a).DOMove (pos, 0.05f).SetEase (Ease.Linear));
					}

					_initLineSequence.OnComplete (() =>
					{
						if (_currentLineRenderer.gameObject.activeSelf)
							_currentLineRenderer.gameObject.SetActive (false);
					});

					_isFinishedInitLine = false;
				}
			}*/

			if (isClampLine && _pointsList.Count > 0)
				_pointsList.RemoveAt (0);

			//_lineRenderer.SetVertexCount (isClampLine ? _pointsCount : _pointsList.Count);
			_lineRenderer.SetVertexCount (_pointsList.Count);

			Vector3 addPoint;

			if (_pointsList.Count >= 2)
			{
				addPoint = new Vector3 ((_pointsList [_pointsList.Count - 2].x + _pointsList [_pointsList.Count - 1].x) * _vertexSmooth,
					(_pointsList [_pointsList.Count - 2].y + _pointsList [_pointsList.Count - 1].y) * _vertexSmooth,
					(_pointsList [_pointsList.Count - 2].z + _pointsList [_pointsList.Count - 1].z) * _vertexSmooth);
			}
			else
			{
				addPoint = _pointsList [_pointsList.Count - 1];
			}

			//_lineRenderer.SetPosition ((isClampLine ? _pointsCount - 1 : _pointsList.Count - 1), addPoint);
			_lineRenderer.SetPositions (_pointsList.ToArray());

			yield return null;

			// If collidable also set vertex positions
			if(LineCollider != null)
			{
				_colliderList.Add(new Vector2(pos.x,pos.y));

				Vector2[] colliderVertexPositions = new Vector2[_colliderList.Count];

				for(int i = 0; i< _colliderList.Count ; i++)
				{
					colliderVertexPositions[i] = _colliderList[i];
				}

				if(colliderVertexPositions.Length >= 2)
					LineCollider.points = colliderVertexPositions;
			}

		}
	}

	private IEnumerator ContinueDuplicateLine()
	{
		if (_pointsList.Count < 2)
			yield break;
		
		var tempPointsList = _pointsList.ToList ();
		List<Vector3> nextPointsList = _pointsList.ToList ();
		//Vector3 deltaLine = nextPointsList[nextPointsList.Count - 1] - nextPointsList [0];

		for (int a = 0; a < 5; a++)
		{
			tempPointsList [0] = nextPointsList [nextPointsList.Count - 1];//+ (nextPointsList[1] - nextPointsList[0]);

			for (int i = 0; i < tempPointsList.Count - 1; i++)
			{
				tempPointsList [i + 1] = tempPointsList [i] + (nextPointsList [i + 1] - nextPointsList [i]);
			}

			_pointsCount = _pointsList.Count;

			for (int i = 0; i < tempPointsList.Count; i++)
			{
				DrawPoint (tempPointsList [i], true);

				/*
				if (i + 1 < tempPointsList.Count)
				{
					i++;
					DrawPoint (tempPointsList [i], true);
				}
			*/
				yield return null;
			}

			nextPointsList = new List<Vector3>(tempPointsList);
		}
			
		if (!_isFinishedDuplicate)
		{
			_isFinishedDuplicate = true;
			Notify (N.FinishDrawDuplicateLine, NotifyType.GAME);
		}
	}

}

