using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class LevelView : MonoBehaviour
{
	public List<Transform> StepsList;

	void OnEnable()
	{
		transform.name = string.Format("Level_{0:00}", transform.GetSiblingIndex () + 1);

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

