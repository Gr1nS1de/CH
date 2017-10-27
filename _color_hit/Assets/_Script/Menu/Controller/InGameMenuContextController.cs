using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuContextController : Controller
{
	private InGameMenuContext _inGameContext;

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.StartLevel__:
				{
					LevelView levelView =  (LevelView)data [0];
					int step = (int)data [1];

					_inGameContext.GoToState (InGameMenuState.Game);
					break;
				}
		}
	}

	void Start()
	{
		_inGameContext = ui.controller.MainContextController.MainContext.InGameMenuContext;
	}

	public void ClickPause()
	{
		_inGameContext.GoToState (InGameMenuState.Pause);
	}

	public void ClickContinue()
	{
		_inGameContext.GoToState (InGameMenuState.Game);
	}

	public void ClickRetry()
	{
		_inGameContext.GoToState (InGameMenuState.Game);

		Notify (N.RetryLevel);
	}

	public void ClickBack()
	{
		_inGameContext.GoToState (InGameMenuState.None);
		Notify (N.BackAction);
	}
}
