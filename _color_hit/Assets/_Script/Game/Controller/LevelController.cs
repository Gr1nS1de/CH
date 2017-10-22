using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : Controller
{

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
		List<StyleView> stylesViewsList = game.view.StylesViewList;

		stylesViewsList.ForEach (styleView =>
		{
			styleView.LevelsList.ForEach(levelView=>
			{
				levelView.StepsList.ForEach(stepTransform=>
				{
					stepTransform.gameObject.SetActive(false);
				});
			});
		});
	}
}

