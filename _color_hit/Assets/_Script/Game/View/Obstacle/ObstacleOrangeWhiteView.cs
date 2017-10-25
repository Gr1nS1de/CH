using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class ObstacleOrangeWhiteView : ObstacleView
{
	public Transform MainRenderer;

	[SerializeField]
	private ObstacleModel.WhiteOrangeObstacleType _obstacleType;
	private Sequence _collisionSequence;

	void OnEnable()
	{
		if(MainRenderer == null && GetComponent<SpriteRenderer>() != null)
			MainRenderer = transform;
		
		CollisionType = Utils.GetObstacleCollisionType (MainRenderer.gameObject.layer);
		IsTriggered = false;

		if (_collisionSequence == null)
		{
			_collisionSequence = DOTween.Sequence ();

			switch (_obstacleType)
			{
				case ObstacleModel.WhiteOrangeObstacleType.Die:
					{
						_collisionSequence
							.Append(MainRenderer.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.3f, 1, 1))
							.SetAutoKill(false)
							.SetRecyclable(true)
							.Pause();
						break;
					}

				case ObstacleModel.WhiteOrangeObstacleType.Point:
					{
						_collisionSequence
							.Append(MainRenderer.DOScale(Vector3.zero, 0.3f))
							.SetAutoKill(false)
							.SetRecyclable(true)
							.Pause();
						break;
					}

				case ObstacleModel.WhiteOrangeObstacleType.HalfPoint:
					{
						_collisionSequence
							.Append(MainRenderer.DOScaleX(0f, 0.3f))
							.SetAutoKill(false)
							.SetRecyclable(true)
							.Pause();
						break;
					}
			}
				
		}
	}

	#region public methods
	public override void Init ()
	{
		_collisionSequence.Complete ();
		_collisionSequence.SmoothRewind ();
	}

	public override void Collision ()
	{
		_collisionSequence.Play ();
	}
	#endregion

	void OnDisable()
	{
		_collisionSequence.Rewind ();
	}
}

