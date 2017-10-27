using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : Controller
{
	private LevelModel _levelModel {get {return game.model.levelModel;}}

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.StartLevel__:
				{
					LevelView levelView =  (LevelView)data [0];
					int step = (int)data [1];

					_levelModel.CurrentLevel = levelView;
					_levelModel.CurrentStep = step;

					ActivateLevel (levelView, step);
					break;
				}

			case N.BackAction:
				{
					switch (GM.Instance.GameState)
					{
						case GameState.Play:
							{
								DisableLevelSteps (game.view.GetCurrentLevelView ());
								break;
							}
					}
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
				DisableLevelSteps(levelView);
			});
		});
	}

	private void DisableLevelSteps(LevelView levelView)
	{
		levelView.StepsList.ForEach(stepTransform=>
			{
				stepTransform.gameObject.SetActive(false);
			});
	}

	private void ActivateLevel(LevelView levelView, int step )
	{
		Debug.LogFormat ("ActivateLevel. level: {0}. step: {1}", levelView.LevelIndex + 1, step);

		StyleView currentStyleView = game.view.GetCurrentStyleView ();

		for(int i = 0; i< currentStyleView.LevelsList.Count; i++)
		{
			LevelView level = currentStyleView.LevelsList[i];

			for(int s = 0; s < level.StepsList.Count; s++)
			{
				Transform stepTransform = level.StepsList[s];

				if(level.Equals(levelView))
				{
					stepTransform.gameObject.SetActive(s == step);
				}else{
					stepTransform.gameObject.SetActive(false);
				}
			}
		}
	}
}

