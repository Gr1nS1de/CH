using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class LevelView : View
{
	public List<Transform> StepsList;
	public int LevelIndex;

	#if UNITY_EDITOR
	public bool IsFirstStepEnabled = false;
	public bool IsSecondStepEnabled = false;
	#endif

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

	#if UNITY_EDITOR
	void Update()
	{
		if (GM.Instance != null)
			return;
		
			if (IsFirstStepEnabled != StepsList [0].gameObject.activeSelf)
			{
				StepsList [0].gameObject.SetActive (IsFirstStepEnabled);
			}

			if (IsSecondStepEnabled != StepsList [1].gameObject.activeSelf)
			{
				StepsList [1].gameObject.SetActive (IsSecondStepEnabled);
			}

	}
	#endif
}

