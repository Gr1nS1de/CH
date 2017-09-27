using UnityEngine;
using System.Collections;

public class LineModel : Model
{
	private bool _isDraw = false;

	public void StartDraw()
	{
		_isDraw = true;
	}

	public bool IsDraw()
	{
		return _isDraw;
	}

	public void FinishDraw()
	{
		_isDraw = false;
	}

}

