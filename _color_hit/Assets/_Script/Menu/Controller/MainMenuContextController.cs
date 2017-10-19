using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Slash.Unity.DataBind.Core.Presentation;
using DG.Tweening;

public class MainMenuContextController : Controller
{
	public MainMenuContext MainMenuContext;
	public Transform LevelsMovePathTweenObj;
	public Transform StylesContainer;
	public List<ContextHolder> StyleContextHoldersList;

	public const float ITEMS_EXPAND_GAP = 15f;
	public const float STYLE_ITEM_PERCANTAGE_GAP = 0.057f;
	public const float SELECTED_STYLE_RADIUS = 155f;
	public const float ITEM_STYLE_RADIUS = 100f;

	private Tween _pathForPointsTween;
	private Sequence _selectStyleSequence;
	private string _styleItemMoveTweenId = "style.item.move.tween.id";
	private string _styleItemSelectTweenId = "style.item.select.tween.id";

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.SetStyle_:
				{
					StyleData styleData = (StyleData)data [0];

					break;
				}

			case N.DragInput____:
				{	
					GameObject selectedGameObject = (GameObject)data [0];
					Vector3 currentPosition = (Vector3)data [1];
					Vector2 deltaPosition = (Vector2)data [2];
					ContinuousGesturePhase gesturePhase = (ContinuousGesturePhase) data[3];

					if (deltaPosition.x == 0f)
						return;

					int maxItems = StyleContextHoldersList.Count;
					int i = 0;

					//TODO. Inertia
					if (gesturePhase == ContinuousGesturePhase.Ended)
					{
						DOTween.Kill (_styleItemMoveTweenId);

						foreach(ItemStyleData itemData in MainMenuContext.ItemsStyle)
						{
							if (itemData.IsSelected)
								continue;
							
							float pathPointValue = itemData.PositionPercantage + deltaPosition.x * 0.0008f; 
							float plathClampValue = Mathf.Clamp( pathPointValue, 0f+STYLE_ITEM_PERCANTAGE_GAP*i, 1f-((maxItems - 1- i) * STYLE_ITEM_PERCANTAGE_GAP));	

							DOTween.To(()=>itemData.PositionPercantage, (val)=>
							{
								itemData.ItemPosition = _pathForPointsTween.PathGetPoint (val);
							}, plathClampValue, 0.1f).SetUpdate(UpdateType.Normal).SetEase(Ease.Linear).SetId(_styleItemMoveTweenId);

							//Debug.LogFormat ("Item name: {0}. pathPointValue: {1}. clamp: {2}. pathpoint: {3}", itemStyleContextHolder.name, pathPointValue,plathClampValue, _pathTween.PathGetPoint (pathPointValue));

							itemData.PositionPercantage = plathClampValue;
							i++;
						}

						return;
					}

					if (gesturePhase != ContinuousGesturePhase.Started && DOTween.IsTweening (_styleItemMoveTweenId))
					{
						return;
					}
						
					//Debug.LogFormat ("Delta position x: {0}", deltaPosition.x);
					bool isLeft = deltaPosition.x < 0f;

					foreach(ItemStyleData itemData in MainMenuContext.ItemsStyle)
					{
						if (itemData.IsSelected)
							continue;

						float pathPointValue = itemData.PositionPercantage + deltaPosition.x * 0.0005f; 
						float plathClampValue = Mathf.Clamp( pathPointValue, 0f+STYLE_ITEM_PERCANTAGE_GAP*i, 1f-((maxItems - 1- i) * STYLE_ITEM_PERCANTAGE_GAP));	

						DOTween.To(()=>itemData.PositionPercantage, (val)=>
						{
							itemData.ItemPosition = _pathForPointsTween.PathGetPoint (val);
						}, plathClampValue, 0.03f).SetUpdate(UpdateType.Late).SetEase(Ease.Linear).SetId(_styleItemMoveTweenId);

						//Debug.LogFormat ("Item name: {0}. pathPointValue: {1}. clamp: {2}. pathpoint: {3}", itemStyleContextHolder.name, pathPointValue,plathClampValue, _pathTween.PathGetPoint (pathPointValue));

						itemData.PositionPercantage = plathClampValue;
						i++;
					}

					break;
				}
		}
	}

	void Start()
	{
		MainMenuContext = ui.controller.MainContextController.MainContext.MainMenuContext;

		if (StyleContextHoldersList == null || StyleContextHoldersList.Count == 0)
		{
			StyleContextHoldersList = new List<ContextHolder> (StylesContainer.GetComponentsInChildren<ContextHolder> (true));
		}

		CreateStyleItemsPath ();
		InitStyleItems ();
	}
		

	private void CreateStyleItemsPath()
	{
		float halfY = Screen.height * 0.5f;
		float halfX = Screen.width * 0.5f;
		Vector3 screenCenter = new Vector3(halfX, halfY, 0f);
		LevelsMovePathTweenObj.transform.position = new Vector3 (-ITEM_STYLE_RADIUS*StyleContextHoldersList.Count*2f, halfY, 0f);
		float radiusLength = (SELECTED_STYLE_RADIUS + ITEM_STYLE_RADIUS + ITEMS_EXPAND_GAP);

		List<Vector3> wayPoints = new List<Vector3> ()
		{
			new Vector3(0f, halfY, 0f), 
			screenCenter + new Vector3(Mathf.Sin(-0.75f),Mathf.Cos(-0.75f), 0f) * radiusLength, 
			screenCenter + new Vector3(Mathf.Sin(-0.5f),Mathf.Cos(-0.5f), 0f) * radiusLength, 
			screenCenter + new Vector3(Mathf.Sin(-0.25f),Mathf.Cos(-0.25f), 0f) * radiusLength, 
			screenCenter + new Vector3(Mathf.Sin(0f), Mathf.Cos(0f), 0f) * radiusLength, 
			screenCenter + new Vector3(Mathf.Sin(0.25f), Mathf.Cos(0.25f), 0f) * radiusLength, 
			screenCenter + new Vector3(Mathf.Sin(0.5f), Mathf.Cos(0.5f), 0f )*radiusLength, 
			screenCenter + new Vector3(Mathf.Sin(0.75f), Mathf.Cos(0.75f), 0f )*radiusLength, 
			new Vector3(Screen.width, halfY, 0f), 
			new Vector3(Screen.width, halfY, 0f) + new Vector3(ITEM_STYLE_RADIUS*StyleContextHoldersList.Count*2f, 0f, 0f)
		};

		_pathForPointsTween = LevelsMovePathTweenObj.transform.DOPath (wayPoints.ToArray(), 0.5f).SetAutoKill(false);
		_pathForPointsTween.ForceInit ();
	}

	private void InitStyleItems()
	{
		StyleContextHoldersList.ForEach (contextHolder =>
		{
			MainMenuContext.ItemsStyle.Add((ItemStyleData)contextHolder.Context);
		});

		int i = 0;

		foreach(ItemStyleData itemData in MainMenuContext.ItemsStyle)
		{
			itemData.ActionClickStyle += OnClickStyleItem;

			float pathPointValue = Mathf.Clamp01(0.5f + itemData.PositionPercantage + (STYLE_ITEM_PERCANTAGE_GAP * i));

			if (itemData.StyleId == GM.Instance.CurrentStyle)
			{
				itemData.IsLocked = false;
				itemData.IsSelected = true;

				itemData.ItemPosition = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0f);
				itemData.ItemScale = Vector3.one;
			}
			else
			{
				itemData.IsLocked = true;
				itemData.IsSelected = false;

				itemData.ItemPosition = _pathForPointsTween.PathGetPoint (pathPointValue);
				itemData.PositionPercantage = pathPointValue;

				itemData.ItemScale = Vector3.one * 0.6f;

				i++;
			}
		}
	}

	public void OnClickStyleItem(StyleId styleId)
	{
		Debug.LogFormat ("OnClick styleid: {0}", styleId);

		if (GM.Instance.CurrentStyle != styleId)
		{
			Sequence selectStyleSequence = DOTween.Sequence ();
			ItemStyleData selectItemData = null;
			ItemStyleData currentItemData = null;
			int selectItemIndex = -1;
			int currentItemIndex = -1;
			int i = 0;

			foreach (ItemStyleData itemData in MainMenuContext.ItemsStyle)
			{
				if (itemData.IsSelected)
				{
					currentItemData = itemData;
					currentItemIndex = i;
				}

				if (itemData.StyleId == styleId)
				{
					selectItemData = itemData;
					selectItemIndex = i;
				}

				i++;
			}

			bool isRightOffset = currentItemIndex < selectItemIndex;
			List<ItemStyleData> itemsOffsetList = new List<ItemStyleData> ();

			if (isRightOffset)
			{
				for (i = currentItemIndex + 1; i < selectItemIndex - currentItemIndex; i++)
				{
					itemsOffsetList.Add(MainMenuContext.ItemsStyle[i]);
				}

				itemsOffsetList.Reverse ();

			}
			else
			{
				for (i = selectItemIndex + 1; i < currentItemIndex - selectItemIndex; i++)
				{
					itemsOffsetList.Add(MainMenuContext.ItemsStyle[i]);
				}
			}

			int moveItemsCount = itemsOffsetList.Count;
			Debug.LogErrorFormat ("moveItemsCount: {0}. currentItemIndex: {1}. selectItemIndex: {2}", moveItemsCount, currentItemIndex, selectItemIndex);
			selectStyleSequence
				.Append (DOTween.To(()=>currentItemData.ItemScale, (valVector)=>currentItemData.ItemScale=valVector, ItemStyleData.STYLE_ITEM_INIT_SCALE, 0.3f))
				.Append(DOTween.To(()=>selectItemData.ItemPosition, (valVector)=>selectItemData.ItemPosition = valVector, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f), 0.3f))
				.Join(DOTween.To(()=>currentItemData.ItemPosition, (valVector)=>currentItemData.ItemPosition = valVector, itemsOffsetList[moveItemsCount - 1].ItemPosition, 0.3f))
					;

			for (i = 0; i < moveItemsCount; i++)
			{
				ItemStyleData itemData =itemsOffsetList [i];
			
				selectStyleSequence
					.Join (DOTween.To(()=>itemData.ItemPosition, (valVector)=> itemData.ItemPosition = valVector, isRightOffset ? MainMenuContext.ItemsStyle[currentItemIndex+moveItemsCount - i + 1].ItemPosition : MainMenuContext.ItemsStyle[selectItemIndex + i].ItemPosition, 0.3f ));
			}

			currentItemData.IsSelected = false;
			selectItemData.IsSelected = true;

			selectStyleSequence
			.SetId (_styleItemSelectTweenId)
			.Play ();
		}

		Notify (N.SelectStyleInput_,NotifyType.ALL, styleId);
	}

	void OnDestroy()
	{
		foreach (ItemStyleData itemData in MainMenuContext.ItemsStyle)
		{
			itemData.ActionClickStyle -= OnClickStyleItem;

		}
	}
}

