using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : Controller
{

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.StartLevel__:
				{
					int level = (int)data [0];
					int step = (int)data [1];

					ActivateLevel (level, step);
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

	private void ActivateLevel(int level, int step)
	{
		Debug.LogFormat ("ActivateLevel. level: {0}. step: {1}", level, step);

		List<StyleView> stylesViewsList = game.view.StylesViewList;

		stylesViewsList.ForEach (styleView =>
		{
			for(int i = 0; i< styleView.LevelsList.Count; i++)
			{
				LevelView levelView = styleView.LevelsList[i];

				if(level == i)
				{
					for(int s = 0; s < levelView.StepsList.Count; s++)
					{
						Transform stepTransform = levelView.StepsList[s];

						stepTransform.gameObject.SetActive(s == step);
					}
				}
				else{
					levelView.StepsList.ForEach(stepTransform=>
					{
						stepTransform.gameObject.SetActive(false);
					});
				}
			}
		});
	}
}

