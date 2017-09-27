using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ObstacleWhiteOrangeView : ObstacleView
{
	public SpriteRenderer MainRenderer;

	private ObstacleModel.ObstacleCollisionType _collisionType;
	private Sequence _collisionSequence;

	void OnEnable()
	{
		_collisionType = Utils.GetObstacleCollisionType (MainRenderer.gameObject.layer);

		if (_collisionSequence == null)
		{
			_collisionSequence = DOTween.Sequence ();

			switch (_collisionType)
			{
				case ObstacleModel.ObstacleCollisionType.Die:
					{
						_collisionSequence
							.Append(MainRenderer.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.3f, 5, 1))
							.SetAutoKill(false)
							.Pause();
						break;
					}

				case ObstacleModel.ObstacleCollisionType.Point:
					{
						_collisionSequence
							.Append(MainRenderer.transform.DOScale(Vector3.zero, 0.3f))
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
		switch (_collisionType)
		{
			case ObstacleModel.ObstacleCollisionType.Die:
				{
					
					break;
				}

			case ObstacleModel.ObstacleCollisionType.Point:
				{
					
					break;
				}
		}

		_collisionSequence.Play ();
	}
	#endregion

	void OnDisable()
	{
		_collisionSequence.Rewind ();
	}
}

