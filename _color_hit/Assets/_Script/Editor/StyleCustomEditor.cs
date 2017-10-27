using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(StyleOrangeWhite))]
public class StyleCustomEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		StyleOrangeWhite myScript = (StyleOrangeWhite)target;
		if(GUILayout.Button("Build Object"))
		{
			
		}
	}
}