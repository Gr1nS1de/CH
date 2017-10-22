using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class LevelView : MonoBehaviour
{
	public List<Transform> StepsList;
	public int LevelIndex;

	void OnEnable()
	{
		LevelIndex = transform.GetSiblingIndex ();
		transform.name = string.Format("_Level_{0:00}", LevelIndex + 1);

		int stepsCount = transform.childCount;

		if(StepsList == null || StepsList.Count != stepsCount)
		{
			StepsList = new List<Transform> ();

			for (int i = 0; i < stepsCount; i++)
			{
				StepsList.Add (transform.GetChild(i));
			}
		}
	}
}

