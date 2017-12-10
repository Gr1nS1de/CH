using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LineState
{
	Init,
	Drawing,
	Finished
}

public class LineModel : Model
{
	public LineState		currentState			= LineState.Init;
	public List<Vector3> 	pointsList				= new List<Vector3>();
	public List<Vector2> 	colliderList			= new List<Vector2>();
	public bool 			isDrawing 				{ get{ return currentState == LineState.Drawing;}}
	public int				pointCount				{ get { return pointsList.Count; } }
	public float			lineDrawTimeLength 		{ get { return Mathf.Clamp( pointCount * 0.033f,  0f, 1f);}}
	public float			spiralInitAnimationTime = 0.2f;
	public bool				isFirstDrawInited 		{ get; private set; }

	public void InitLine()
	{
		GoToState (LineState.Init);
	}

	public void StartDraw()
	{
		GoToState (LineState.Drawing);
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
		GoToState (LineState.Finished);
	}

	private void GoToState(LineState state)
	{
		switch (state)
		{
			case LineState.Init:
				{
					isFirstDrawInited = false;
					break;
				}

			case LineState.Drawing:
				{
					ClearLinePoints ();
					break;
				}

			case LineState.Finished:
				{
					break;
				}
		}

		currentState = state;
	}

	private void ClearLinePoints()
	{
		pointsList.Clear();
		colliderList.Clear();
	}
}

