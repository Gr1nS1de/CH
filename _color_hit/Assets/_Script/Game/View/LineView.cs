using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class LineView : View
{
	public EdgeCollider2D LineCollider;

	private LineRenderer 	_lineRenderer { get { return game.view.GetCurrentLineRenderer (); } }
	private List<Vector3> 	_pointsList { get { return _lineModel.pointsList; } }
	private List<Vector3> 	_spiralPointsList = new List<Vector3> ();
	private List<Vector2> 	_colliderList { get { return _lineModel.colliderList; } }
	private float 			_vertexSmooth = 0.5f;
	private int 			_lastIndex = -1;

	private IEnumerator 	_duplicateLineRoutine = null;
	private Tween 			_collapseLineToStartTween = null;
	private Tween 			_collapseLineToEndTween = null;
	private Sequence 		_punchExpandLineSequence = null;
	private	string			_lineTweenId = "line.tween.id";

	private LineModel 		_lineModel { get { return game.model.lineModel;}}

	#region public methods
	public void InitSpiral()
	{
		_spiralPointsList.Clear ();
		ResetLine ();
		/*
		float circleSize = 0f;

		for (int i = 0; i < 100; i++)
		{
			Vector3 addPoint = new Vector3 (Mathf.Sin (i) * circleSize / 50f, Mathf.Cos (i) * circleSize / 50f, 0f);

			Debug.LogFormat ("Add point to spiral: {0}", addPoint);

			_lineRenderer.SetPosition (i, addPoint);
		}*/
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
		LineCollider.Reset ();
		DOTween.Kill (_lineTweenId);

		if(_duplicateLineRoutine != null)
			StopCoroutine (_duplicateLineRoutine);
	}

	public void DrawPoint(Vector3 pos)
	{
		StartCoroutine (DrawPointRoutine(pos));
	}

	public void FinishDraw()
	{
		Debug.LogFormat("FinishDraw. line points count: {0}", _pointsList.Count);
		_duplicateLineRoutine = ContinueDuplicateLine();

		StartCoroutine( _duplicateLineRoutine);
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

	private IEnumerator DrawPointRoutine(Vector3 pos)
	{
		if (!_pointsList.Contains (pos))
		{
			pos.z = 0f;

			_pointsList.Add (pos);

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

			_lineRenderer.SetPosition (_pointsList.Count - 1, addPoint);

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
		Vector3 deltaLine = _pointsList[_pointsList.Count - 1] - _pointsList [0];

		tempPointsList[0] = _pointsList[_pointsList.Count - 1] + (_pointsList[1] - _pointsList[0]);

		for (int i = 1; i < tempPointsList.Count-1; i++)
		{
			tempPointsList[i] = tempPointsList[i-1] + (_pointsList[i+1] - _pointsList[i]);
		}

		for (int i = 0; i < tempPointsList.Count; i++)
		{
			DrawPoint (tempPointsList[i]);

			if (i + 1 < tempPointsList.Count)
			{
				i++;
				DrawPoint (tempPointsList [i]);
			}
			
			yield return null;
		}
			
		Notify (N.FinishDrawDuplicateLine, NotifyType.GAME);
	}

}

