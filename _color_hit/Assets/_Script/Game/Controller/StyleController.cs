using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StyleController : Controller
{
	private StyleModel _styleModel {get { return core.styleModel;}}
	
	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.SetStyle_:
				{
					StyleData styleData = (StyleData)data [0];
					List<StyleView> stylesViewsList = game.view.StylesViewList;
					StyleView styleView = game.view.GetStyleView (styleData.styleId);

					stylesViewsList.ForEach (sView =>
					{
						sView.gameObject.SetActive (sView.StyleId == styleData.styleId);
					});

					styleView.SetStyle ();
					break;
				}
		}
	}
}

