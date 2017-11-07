﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineModel : Model
{

	public List<Vector3> 	pointsList				= new List<Vector3>();
	public List<Vector2> 	colliderList			= new List<Vector2>();
	public bool 			isDraw 					{ get; private set;}
	public int				pointCount				{ get { return pointsList.Count; } }
	public float			lineDrawTimeLength 		{ get { return Mathf.Clamp( pointCount * 0.033f,  0f, 1f);}}
	public float			spiralInitAnimationTime = 0.2f;
	public bool				isFirstDrawInited 		{ get; private set; }

	public void InitLine()
	{
		isFirstDrawInited = false;
	}

	public void StartDraw()
	{
		ClearLinePoints ();
		isDraw = true;
	}

	public void DrawPoint()
	{
		if (!isFirstDrawInited)
		{
			isFirstDrawInited = true;
		}
	}

	public void FinishDraw()
	{
		isDraw = false;
	}

	private void ClearLinePoints()
	{
		pointsList.Clear();
		colliderList.Clear();
	}
}

