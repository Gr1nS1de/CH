using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class LineView : View
{
	public LineRenderer Line;
	public EdgeCollider2D LineCollider;

	private List<Vector3> 	_pointsList= new List<Vector3>();
	private List<Vector2> 	_colliderList= new List<Vector2>();
	private Vector3 		_mousePosition;
	private float 			_vertexSmooth = 0.5f;
	private Vector2[] 		_colliderVertexPositions;
	private bool 			_isDrawing = false;

	private Tween 			_duplicateLineTween = null;
	private Tween 			_collapseLineToStartTween = null;
	private Tween 			_collapseLineToEndTween = null;
	private Sequence 		_punchExpandLineSequence = null;

	void Start()
	{
		Line.SetVertexCount(0);
	}

	#region public methods
	public void StartDraw()
	{
		ClearLinePoints ();
	}

	public void DrawPoint(Vector3 pos)
	{
		if (!_pointsList.Contains (_mousePosition))
		{
			pos.z = 0f;

			_pointsList.Add (pos);

			Line.SetVertexCount (_pointsList.Count);

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

			Line.SetPosition (_pointsList.Count - 1, addPoint);

			// If collidable also set vertex positions
			if(LineCollider != null)
			{
				_colliderList.Add(new Vector2(pos.x,pos.y));

				_colliderVertexPositions = new Vector2[_colliderList.Count];

				for(int i = 0; i< _colliderList.Count ; i++)
				{
					_colliderVertexPositions[i] = _colliderList[i];
				}

				if(_colliderVertexPositions.Length >= 2)
					LineCollider.points = _colliderVertexPositions;
			}

		}
	}

	public void FinishDraw()
	{
		ContinueDuplicateLine ();
	}

	public void PunchExpandLine()
	{
		_punchExpandLineSequence = DOTween.Sequence ();

		_punchExpandLineSequence
			.Append(DOTween.To (() => 0.05f, width =>
				{
					Line.startWidth = width;
					Line.endWidth = width;
				}, 0.1f, 0.15f)
				.SetEase(Ease.Flash)
			)
			.Append(DOTween.To (() => 0.1f, width =>
				{
					Line.startWidth = width;
					Line.endWidth = width;
				}, 0.05f, 0.1f)
				.SetEase(Ease.Linear)
			);
	}

	public void CollapseLineToStart()
	{
		_duplicateLineTween.Kill ();

		_collapseLineToStartTween = 
			DOTween.To (() => Line.positionCount-1, pointIndex =>
			{
				Line.SetVertexCount(pointIndex);

			}, 0, 0.5f).SetEase(Ease.Linear)
			.OnStart(()=>
			{
				LineCollider.enabled = false;
			})
			.OnComplete(()=>
			{
				//ClearLinePoints();
			});
	}

	public void CollapseLineToEnd()
	{
		_duplicateLineTween.Kill ();

		_collapseLineToEndTween = 
			DOTween.To (() => Line.positionCount-1, pointIndex =>
			{
				Vector3[] linePositions = new Vector3[Line.positionCount];	
				Line.GetPositions (linePositions);
				var linePositionsList = linePositions.ToList();
				linePositionsList.RemoveAt(0);

				Line.SetPositions(linePositionsList.ToArray());

			}, 0, 0.5f).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed)
			.OnStart(()=>
			{
				LineCollider.enabled = false;
			})
			.OnComplete(()=>
			{
				//ClearLinePoints();
			});
	}
		
	public override void OnRendererTriggerEnter (ViewTriggerDetect triggerDetector, Collider2D otherCollider)
	{
		//Debug.LogErrorFormat ("OnRendererTriggerEnter ");
		ObstacleModel.ObstacleCollisionType obstacleType = Utils.GetObstacleCollisionType(otherCollider.gameObject.layer);

		ObstacleView obstacleView = otherCollider.GetComponentInParent<ObstacleView> ();

		if (obstacleView == null)
		{
			Debug.LogErrorFormat ("LineView. OnRendererTriggerEnter. Error: obstacleView == null!");
			return;
		}

		Notify (N.LineImpactObstacle___, NotifyType.GAME, obstacleType, _pointsList[_pointsList.Count-1], obstacleView);
	}
		
	#endregion

	private void ContinueDuplicateLine()
	{
		var tempPointsList = _pointsList.ToList ();
		Vector3 deltaLine = _pointsList[_pointsList.Count - 1] - _pointsList [0];

		Debug.LogFormat ("tempPointsList count: {0}. pointsListCount: {1}", tempPointsList.Count, _pointsList.Count);
		tempPointsList[0] = _pointsList[_pointsList.Count - 1] + (_pointsList[1] - _pointsList[0]);

		for (int i = 1; i < tempPointsList.Count-1; i++)
		{
			tempPointsList[i] = tempPointsList[i-1] + (_pointsList[i+1] - _pointsList[i]);
		}

		_duplicateLineTween = DOTween.To (()=>0,(pointIndex)=>
		{
			_mousePosition = tempPointsList [pointIndex];
			DrawPoint (tempPointsList[pointIndex]);
		}, tempPointsList.Count, Mathf.Clamp( tempPointsList.Count * 0.033f,  0f, 3f))
			.SetEase(Ease.Linear);
	}

	private void ClearLinePoints()
	{
		StopAllCoroutines ();

		Line.SetVertexCount(0);
		_pointsList = new List<Vector3>();
		if(LineCollider != null)
		{
			LineCollider.enabled = true;
			_colliderList = new List<Vector2>();
			bool isTrigger = LineCollider.isTrigger;
			LineCollider.Reset();

			LineCollider.isTrigger = isTrigger;
		}
	}
}

