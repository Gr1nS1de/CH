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
			case N.SetStyle__:
				{
					StyleData styleData = (StyleData)data [0];
					bool isInit = (bool)data [1];

					break;
				}

			case N.SelectStyle_:
				{
					StyleId styleId = (StyleId)data [0];

					Debug.LogFormat ("SelectStyleInput. styleId: {0}. current style: {1}", styleId, GM.Instance.CurrentStyleId);

					if (styleId == GM.Instance.CurrentStyleId)
					{
						GoToState (ContextType.SelectLevelContext);
					}
					break;
				}

			case N.BackAction:
				{
					switch(GM.Instance.GameState)
					{
						case GameState.Play:
							{
								GoToState (ContextType.SelectLevelContext);
								break;
							}
					}
					break;
				}
		}
	}
		
	void Start()
	{
		MainContext = new MainContext ();

		MainContextHolder.Context = MainContext;
		MainContext.ResourceStarsCount = core.playerData.starsCount;

		GoToState (ContextType.MainMenuContext);
	}

	public void GoToState( ContextType state)
	{
		Debug.LogFormat("GoToState. State: {0}. Current state: {1}", state, CurrentState);
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
