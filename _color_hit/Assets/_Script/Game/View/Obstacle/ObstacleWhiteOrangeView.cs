using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ObstacleWhiteOrangeView : ObstacleView
{
	public SpriteRenderer MainRenderer;

	[SerializeField]
	private ObstacleModel.WhitrOrangeObstacleType _obstacleType;
	private ObstacleModel.ObstacleCollisionType _collisionType;
	private Sequence _collisionSequence;

	void OnEnable()
	{
		if (GetComponent<SpriteRenderer> ())
			MainRenderer = GetComponent<SpriteRenderer> ();
		
		_collisionType = Utils.GetObstacleCollisionType (MainRenderer.gameObject.layer);

		if (_collisionSequence == null)
		{
			_collisionSequence = DOTween.Sequence ();

			switch (_obstacleType)
			{
				case ObstacleModel.WhitrOrangeObstacleType.Die:
					{
						_collisionSequence
							.Append(MainRenderer.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.3f, 1, 1))
							.SetAutoKill(false)
							.Pause();
						break;
					}

				case ObstacleModel.WhitrOrangeObstacleType.Point:
					{
						_collisionSequence
							.Append(MainRenderer.transform.DOScale(Vector3.zero, 0.3f))
							.SetAutoKill(false)
							.Pause();
						break;
					}

				case ObstacleModel.WhitrOrangeObstacleType.HalfPoint:
					{
						_collisionSequence
							.Append(MainRenderer.transform.DOScaleX(0f, 0.3f))
							.SetAutoKill(false)
							.Pause();
						break;
					}
			}
		}
	}

	#region public methods
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

