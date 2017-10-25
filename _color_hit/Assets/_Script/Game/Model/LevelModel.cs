using UnityEngine;
using System.Collections;

public class LevelModel : Model
{
	public LevelView CurrentLevel = null;
	public int CurrentStep;

	public void InitCurrentLevel(LevelView levelView, int step)
	{
		game.model.levelModel.CurrentLevel = levelView;
		game.model.levelModel.CurrentStep = step;
	}
}

