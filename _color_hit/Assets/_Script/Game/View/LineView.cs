using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class LineView : View
{
	public LineRenderer Line;
	public EdgeCollider2D LineCollider;

	private List<Vector3> pointsList= new List<Vector3>();
	private List<Vector2> colliderList= new List<Vector2>();
	private Vector3 mousePos;
	private float smooth = 0.5f;
	private Vector2[] _colliderVertexPositions;
	private bool _isDrawing = false;

	private Tween _duplicateLineTween = null;

	void Start()
	{
		Line.SetVertexCount(0);
	}

	#region public methods
	public void StartDraw()
	{
		ResetLinePoints ();
	}

	public void DrawPoint(Vector3 pos, bool isCollidable)
	{
		if (!pointsList.Contains (mousePos))
		{
			pos.z = 0f;
			pointsList.Add (pos);
			Line.SetVertexCount (pointsList.Count);
			Vector3 p ;
			if(pointsList.Count>=2)
			{
				p = new Vector3((pointsList [pointsList.Count - 2].x +pointsList [pointsList.Count - 1].x ) * smooth,
					(pointsList [pointsList.Count - 2].y +pointsList [pointsList.Count - 1].y ) * smooth,
					(pointsList [pointsList.Count - 2].z +pointsList [pointsList.Count - 1].z ) * smooth);
			}
			else
				p = pointsList [pointsList.Count - 1];
			Line.SetPosition (pointsList.Count - 1, p);
			// If collidable also set vertex positions
			if(isCollidable && LineCollider != null)
			{
				colliderList.Add(new Vector2(pos.x,pos.y));

				_colliderVertexPositions = new Vector2[colliderList.Count];
				for(int i = 0; i< colliderList.Count ; i++)
				{
					_colliderVertexPositions[i] = colliderList[i];
				}
				if(_colliderVertexPositions.Length >= 2)
					LineCollider.points = _colliderVertexPositions;
			}

		}
	}

	public void FinishDraw()
	{
		var tempPointsList = pointsList.ToList ();
		Vector3 deltaLine = pointsList[pointsList.Count - 1] - pointsList [0];

		Debug.LogFormat ("tempPointsList count: {0}. pointsListCount: {1}", tempPointsList.Count, pointsList.Count);
		tempPointsList[0] = pointsList[pointsList.Count - 1] + (pointsList[1] - pointsList[0]);

		for (int i = 1; i < tempPointsList.Count-1; i++)
		{
			tempPointsList[i] = tempPointsList[i-1] + (pointsList[i+1] - pointsList[i]);
		}

		_duplicateLineTween = DOTween.To (()=>0,(pointIndex)=>
		{
			mousePos = tempPointsList [pointIndex];
			DrawPoint (tempPointsList[pointIndex], true);
		}, tempPointsList.Count, 1f)
			.SetEase(Ease.Linear);
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

		Notify (N.LineImpactObstacle___, NotifyType.GAME, obstacleType, pointsList[pointsList.Count-1], obstacleView);
	}
		
	#endregion

	private void ResetLinePoints()
	{
		StopAllCoroutines ();

		Line.SetVertexCount(0);
		pointsList = new List<Vector3>();
		if(LineCollider != null)
		{
			colliderList = new List<Vector2>();
			bool isTrigger = LineCollider.isTrigger;
			LineCollider.Reset();

			LineCollider.isTrigger = isTrigger;
		}
	}
}

