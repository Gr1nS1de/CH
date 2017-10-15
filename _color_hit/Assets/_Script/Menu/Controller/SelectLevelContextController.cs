using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectLevelContextController : Controller
{
	public SelectLevelContext SelectLevelContext;
	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.SetStyle_:
				{
					break;
				}
		}
	}

	void Start()
	{
		SelectLevelContext = ui.controller.MainContextController.MainContext.SelectLevelContext;
	}
}

