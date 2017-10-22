using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ObstacleOrangeWhiteView : ObstacleView
{
	public Transform MainRenderer;

	[SerializeField]
	private ObstacleModel.WhitrOrangeObstacleType _obstacleType;
	private ObstacleModel.ObstacleCollisionType _collisionType;
	private Sequence _collisionSequence;

	void OnEnable()
	{
		if(MainRenderer == null && GetComponent<SpriteRenderer>() != null)
			MainRenderer = transform;
		
		_collisionType = Utils.GetObstacleCollisionType (MainRenderer.gameObject.layer);

		if (_collisionSequence == null)
		{
			_collisionSequence = DOTween.Sequence ();

			switch (_obstacleType)
			{
				case ObstacleModel.WhitrOrangeObstacleType.Die:
					{
						_collisionSequence
							.Append(MainRenderer.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.3f, 1, 1))
							.SetAutoKill(false)
							.SetRecyclable(true)
							.Pause();
						break;
					}

				case ObstacleModel.WhitrOrangeObstacleType.Point:
					{
						_collisionSequence
							.Append(MainRenderer.DOScale(Vector3.zero, 0.3f))
							.SetAutoKill(false)
							.SetRecyclable(true)
							.Pause();
						break;
					}

				case ObstacleModel.WhitrOrangeObstacleType.HalfPoint:
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

