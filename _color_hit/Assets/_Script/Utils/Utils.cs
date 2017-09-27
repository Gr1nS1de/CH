using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Globalization;

/// <summary>
/// Utility class.
/// </summary>
public static class Utils
{
	public static string GetTextFirstUpper(string text)
	{
		return string.Format("{0}{1}", char.ToUpper(text [0]), text.Substring(1));
	}

	public static string SweetMoney(int number) 
	{
		return number.ToString("N0", new NumberFormatInfo { NumberGroupSeparator = " " });
	}

	public static IEnumerator RebuildLayoutGroups(RectTransform element)
	{
		if (element == null)
		{
			Debug.LogError ("Trying to rebuild layout for null element!");
			yield break;
		}

		yield return new WaitForEndOfFrame ();

		List<LayoutGroup> layoutsList = new List<LayoutGroup>(element.GetComponentsInChildren<LayoutGroup> (true));

		if (layoutsList.Count > 0)
		{
			layoutsList.ForEach ((layout) =>
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate (layout.GetComponent<RectTransform> ());
			});
		}
	}

	public static ObstacleModel.ObstacleCollisionType GetObstacleCollisionType(int layer)
	{
		return (ObstacleModel.ObstacleCollisionType)System.Enum.Parse (typeof(ObstacleModel.ObstacleCollisionType), LayerMask.LayerToName( layer));
	}
}
