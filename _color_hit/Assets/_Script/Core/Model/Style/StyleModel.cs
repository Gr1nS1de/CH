﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StyleData
{
	public StyleId styleId;
	public Color styleColor;
	public int levelsCount;
	public int stepsCount;
}

public enum StyleId
{
	OrangeWhite,
	HospitalVintage,
	CosmicPen,
	CartoonedScum,
	PinkTennis,
	RedWhiteFan,
	HeroChoose
}

[CreateAssetMenu(fileName = "StylesSettings", menuName = "Styles/List", order = 1)]
public class StylesSettings : ScriptableObject 
{
	public List<StyleData> StylesDataList;
}


[ExecuteInEditMode]
public class StyleModel : Model
{
	public StylesSettings StylesSettings;

	void Start()
	{
		string[] stylesIds = System.Enum.GetNames (typeof(StyleId));
	
		if (StylesSettings == null || StylesSettings.StylesDataList.Count == 0)
		{
			StylesSettings.StylesDataList = new List<StyleData> ();

			foreach (string sStyleId in stylesIds)
			{
				StylesSettings.StylesDataList.Add (new StyleData () {
					styleId = (StyleId)System.Enum.Parse (typeof(StyleId), sStyleId),
					styleColor = Color.white,
					levelsCount = 10,
					stepsCount = 2
				});
			}
		}
		else
		{
			if (StylesSettings.StylesDataList.Count != stylesIds.Length)
			{
				//TODO. Add new items and save prev values.
			}
		}
	}

	public StyleData GetStyleData(StyleId styleId)
	{
		return StylesSettings.StylesDataList.Find (sData => sData.styleId == styleId);
	}
}

