using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public abstract class ObstacleView : View
{

	#region public methods
	public abstract void Collision();
	public abstract void Init();
	#endregion

}

