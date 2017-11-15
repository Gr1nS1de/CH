using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : Controller
{
	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.GameOver:
				{
					break;
				}

			case N.FinishStep_:
				{
					break;
				}
		}
	}

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