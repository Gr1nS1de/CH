using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : Controller
{
	//List<Touch> TouchesList = new List<Touch> ();
	//Touch[] _inputTouches = Input.touches;

	public void OnDrag(DragGesture e)
	{
		Notify (N.DragInput____, NotifyType.ALL, e.StartSelection, Camera.main.ScreenToWorldPoint(e.Position), e.DeltaMove, e.Phase);
	}

	/*
	public void OnTap(TapGesture e)
	{
		Notify (N.OnDoubleTapInput, NotifyType.GAME);
	}*/
}