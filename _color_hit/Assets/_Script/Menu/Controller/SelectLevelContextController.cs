using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectLevelContextController : Controller
{
	public SelectLevelContext SelectLevelContext;
	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.SetStyle__:
				{
					StyleData styleData = (StyleData)data [0];
					bool isInit = (bool)data [1];

					if (isInit)
						return;
					
					InitLevels (styleData);
					break;
				}

		}
	}

	void Start()
	{
		SelectLevelContext = ui.controller.MainContextController.MainContext.SelectLevelContext;

		InitLevels ();
	}

	private void InitLevels(StyleData styleData = null)
	{
		StyleData currentStyleData = core.styleModel.GetCurrentStyleData ();
		bool isInit = styleData == null;
		styleData = isInit? currentStyleData : styleData;

		SelectLevelContext.CompletedLevelsText = string.Format ("{0}/{1}", 0, styleData.levelsCount);

		UnregisterItemsSelectClick ();

		SelectLevelContext.ItemsSelect.Clear ();

		for (int i = 0; i < styleData.levelsCount; i++)
		{
			SelectLevelContext.ItemsSelect.Add (new ItemSelectContextData () 
			{
				Level = i

			});
		}

		foreach (ItemSelectContextData itemData in SelectLevelContext.ItemsSelect)
		{
			itemData.ActionClick += OnClickItem;
		}

	}

	private void OnClickItem(int level, int step)
	{
		Debug.LogFormat ("OnClickItem. Select item level: {0}. step: {1}", level, step);

		Notify (N.SelectLevel__, NotifyType.UI, level, step);
	}

	private void UnregisterItemsSelectClick()
	{
		foreach (ItemSelectContextData itemData in SelectLevelContext.ItemsSelect)
		{
			itemData.ActionClick -= OnClickItem;
		}
	}

	void OnDestroy()
	{
		if (SelectLevelContext != null)
		{
			UnregisterItemsSelectClick ();
		}
	}
}

