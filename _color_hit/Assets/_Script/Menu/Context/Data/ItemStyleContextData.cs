using UnityEngine;
using System.Collections;
using Slash.Unity.DataBind.Core.Data;
using DG.Tweening;
using System;

public class ItemStyleContextData : Context
{
	public static Vector3 STYLE_ITEM_INIT_SCALE = new Vector3 (0.6f, 0.6f, 1f);
	public event Action<StyleId> ActionClickStyle;

	private readonly Property<StyleId> styleIdProperty = new Property<StyleId>();
	public StyleId StyleId
	{
		get { return this.styleIdProperty.Value; }
		set { this.styleIdProperty.Value = value; }
	}

	private readonly Property<Vector3> itemPositionProperty = new Property<Vector3>();
	public Vector3 ItemPosition
	{
		get { return this.itemPositionProperty.Value; }
		set { this.itemPositionProperty.Value = value; }
	}

	private readonly Property<Vector3> itemScaleProperty = new Property<Vector3>();
	public Vector3 ItemScale
	{
		get { return this.itemScaleProperty.Value; }
		set { this.itemScaleProperty.Value = value; }
	}

	private readonly Property<bool> isLockedProperty = new Property<bool>();
	public bool IsLocked
	{
		get { return this.isLockedProperty.Value; }
		set { this.isLockedProperty.Value = value; }
	}

	private readonly Property<bool> isSelectedProperty = new Property<bool>();
	public bool IsSelected
	{
		get { return this.isSelectedProperty.Value; }
		set { this.isSelectedProperty.Value = value; }
	}

	private readonly Property<bool> isInteractableProperty = new Property<bool>();
	public bool IsInteractable
	{
		get { return this.isInteractableProperty.Value; }
		set { this.isInteractableProperty.Value = value; }
	}

	private readonly Property<string> textCompleteStatsProperty = new Property<string>();
	public string TextCompleteStats
	{
		get { return this.textCompleteStatsProperty.Value; }
		set { this.textCompleteStatsProperty.Value = value; }
	}

	public float PositionPercantage;

	public ItemStyleContextData()
	{
		IsInteractable = true;
	}

	public void OnClickItem(StyleId styleId)
	{
		ActionClickStyle (styleId);
	}

}

