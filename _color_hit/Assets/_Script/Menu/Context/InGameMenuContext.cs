using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Slash.Unity.DataBind.Core.Data;

public enum InGameMenuState
{
	None,
	Game,
	Pause
}

public class InGameMenuContext : Context
{
	private readonly Property<bool> isShowPausePanelProperty = new Property<bool>();
	public bool IsShowPausePanel
	{
		get { return this.isShowPausePanelProperty.Value; }
		set { this.isShowPausePanelProperty.Value = value; }
	}

	private readonly Property<bool> isShowInGamePanelProperty = new Property<bool>();
	public bool IsShowInGamePanel
	{
		get { return this.isShowInGamePanelProperty.Value; }
		set { this.isShowInGamePanelProperty.Value = value; }
	}

	public InGameMenuContext()
	{
		IsShowPausePanel = false;
		IsShowInGamePanel = false;
	}

	public void GoToState(InGameMenuState state)
	{
		IsShowPausePanel = false;
		IsShowInGamePanel = false;

		switch(state)
		{
			case InGameMenuState.Game:
				{
					IsShowInGamePanel = true;
					break;
				}

			case InGameMenuState.Pause:
				{
					IsShowPausePanel = true;
					break;
				}

			case InGameMenuState.None:
				{
					break;
				}
		}
	}
}

