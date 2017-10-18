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
	private string _styleItemMoveTweenId = "style.item.tween.id";

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

					if (gesturePhase == ContinuousGesturePhase.Ended)
					{
						DOTween.Kill (_styleItemMoveTweenId);

						StyleContextHoldersList.ForEach (itemStyleContextHolder =>
						{
							ItemStyleData itemData = (ItemStyleData)itemStyleContextHolder.Context;
							float pathPointValue = itemData.PositionPercantage + deltaPosition.x * 0.0008f; 
							float plathClampValue = Mathf.Clamp( pathPointValue, 0f+STYLE_ITEM_PERCANTAGE_GAP*i, 1f-((maxItems - 1- i) * STYLE_ITEM_PERCANTAGE_GAP));	

							DOTween.To(()=>itemData.PositionPercantage, (val)=>
							{
								itemData.ItemPosition = _pathForPointsTween.PathGetPoint (val);
							}, plathClampValue, 0.1f).SetUpdate(UpdateType.Normal).SetEase(Ease.Linear).SetId(_styleItemMoveTweenId);

							//Debug.LogFormat ("Item name: {0}. pathPointValue: {1}. clamp: {2}. pathpoint: {3}", itemStyleContextHolder.name, pathPointValue,plathClampValue, _pathTween.PathGetPoint (pathPointValue));

							itemData.PositionPercantage = plathClampValue;
							i++;
						});

						return;
					}

					if (gesturePhase != ContinuousGesturePhase.Started && DOTween.IsTweening (_styleItemMoveTweenId))
					{
						return;
					}
						
					//Debug.LogFormat ("Delta position x: {0}", deltaPosition.x);
					bool isLeft = deltaPosition.x < 0f;

					StyleContextHoldersList.ForEach (itemStyleContextHolder =>
					{
						ItemStyleData itemData = (ItemStyleData)itemStyleContextHolder.Context;
						float pathPointValue = itemData.PositionPercantage + deltaPosition.x * 0.0005f; 
						float plathClampValue = Mathf.Clamp( pathPointValue, 0f+STYLE_ITEM_PERCANTAGE_GAP*i, 1f-((maxItems - 1- i) * STYLE_ITEM_PERCANTAGE_GAP));	

						DOTween.To(()=>itemData.PositionPercantage, (val)=>
						{
							itemData.ItemPosition = _pathForPointsTween.PathGetPoint (val);
						}, plathClampValue, 0.03f).SetUpdate(UpdateType.Late).SetEase(Ease.Linear).SetId(_styleItemMoveTweenId);

						//Debug.LogFormat ("Item name: {0}. pathPointValue: {1}. clamp: {2}. pathpoint: {3}", itemStyleContextHolder.name, pathPointValue,plathClampValue, _pathTween.PathGetPoint (pathPointValue));

						itemData.PositionPercantage = plathClampValue;
						i++;
					});

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

		InitStyleItems ();
		InitSelectedItem ();
	}

	private void InitStyleItems()
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

		StyleContextHoldersList.ForEach (contextHolder =>
		{
			MainMenuContext.ItemsStyle.Add((ItemStyleData)contextHolder.Context);
		});

		int i = 0;

		StyleContextHoldersList.ForEach (itemStyleContextHolder =>
		{
			ItemStyleData itemData = (ItemStyleData)itemStyleContextHolder.Context;
			float pathPointValue = Mathf.Clamp01(0.5f + itemData.PositionPercantage + (STYLE_ITEM_PERCANTAGE_GAP * i));

			itemData.ItemPosition = _pathForPointsTween.PathGetPoint(pathPointValue);
			itemData.PositionPercantage = pathPointValue;
			i++;
		});
	}

	private void InitSelectedItem()
	{

	}
}

