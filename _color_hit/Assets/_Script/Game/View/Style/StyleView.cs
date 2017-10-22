using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public abstract class StyleView : View
{
	public StyleId StyleId;
	public List<LevelView> LevelsList = null;

	public virtual void OnEnable()
	{
		int levelsCount = transform.childCount;

		if (levelsCount > 0)
		{
			if (LevelsList == null || LevelsList.Count == 0)
			{
				LevelsList = new List<LevelView> ();

				Debug.LogFormat ("Fill {0} whith {1} levels.", StyleId, levelsCount);

				for (int i = 0; i < levelsCount; i++)
				{
					LevelView levelView = transform.GetChild (i).GetComponent<LevelView> ();

					if (levelView == null)
					{
						Debug.LogErrorFormat ("Error. No LevelView for level {0}", transform.GetChild (i).name);
						continue;
					}

					levelView.name = string.Format ("_Level_{0:00}", i + 1);

					LevelsList.Add (levelView);
				}
			}
			else
			{
				LevelsList.ForEach (levelView =>
				{
					if(levelView == null)
						LevelsList.Remove(levelView);
				});

				LevelsList.ForEach (levelView =>
				{
					if(levelView == null)
						LevelsList.Remove(levelView);
				});
			}
		}
	}

	#region public methods

	public virtual void SetStyle()
	{

	}

	#endregion
}

