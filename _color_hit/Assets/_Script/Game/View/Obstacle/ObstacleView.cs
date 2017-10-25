using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public abstract class ObstacleView : View
{
	public ObstacleModel.ObstacleCollisionType CollisionType {get; protected set;}
	public bool IsTriggered = false;

	#region public methods
	public abstract void Collision();
	public abstract void Init();
	#endregion

}

