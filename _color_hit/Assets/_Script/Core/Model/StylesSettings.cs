using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StylesSettings", menuName = "Styles/List", order = 1)]
public class StylesSettings : ScriptableObject 
{
	public List<StyleData> StylesDataList;
}
