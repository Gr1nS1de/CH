using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slash.Unity.DataBind.Core.Presentation;

public class MainContextController : Controller 
{
	public ContextHolder MainContextHolder;
	public MainContext MainContext;

	public BaseContext CurrentState;

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.SetStyle_:
				{
					StyleData styleData = (StyleData)data [0];

					break;
				}

			case N.SelectStyleInput_:
				{
					StyleId styleId = (StyleId)data [0];

					Debug.LogFormat ("OnClick styleid: {0}", styleId);

					if (styleId == GM.Instance.CurrentStyle)
					{
						GoToState (ContextType.SelectLevelContext);
					}
					break;
				}
		}
	}
		
	void Start()
	{
		MainContext = new MainContext ();

		MainContextHolder.Context = MainContext;

		GoToState (ContextType.MainMenuContext);
	}

	public void GoToState( ContextType state)
	{
		UnloadState();

		switch ( state )
		{
			case ContextType.MainMenuContext:
				{
					CurrentState = MainContext.MainMenuContext;
					break;
				}

			case ContextType.SelectLevelContext:
				{
					CurrentState = MainContext.SelectLevelContext;
					break;
				}

			default:
				{
					Debug.LogErrorFormat ("GoToState. No state: {0}", state);
					break;
				}
		}

		CurrentState.Load();

	}

	public void UnloadState()
	{
		if ( CurrentState != null )
		{
			CurrentState.UnLoad();
		}
	}
}
