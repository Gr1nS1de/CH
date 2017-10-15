using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Slash.Unity.DataBind.Core.Presentation;
using DG.Tweening;

public class MainMenuContextController : Controller
{
	public MainMenuContext MainMenuContext;
	public DOTweenPath LevelsMovePathTween;
	public List<ContextHolder> StyleContextHoldersList;

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.SetStyle_:
				{
					StyleData styleData = (StyleData)data [0];

					break;
				}
		}
	}

	void Start()
	{
		LevelsMovePathTween.GetTween ().ForceInit ();
		MainMenuContext = ui.controller.MainContextController.MainContext.MainMenuContext;

		StyleContextHoldersList.ForEach (contextHolder =>
		{
			MainMenuContext.ItemsStyle.Add((ItemStyleData)contextHolder.Context);
		});
	}
}

