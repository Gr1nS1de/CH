using UnityEngine;
using System.Collections;
using Slash.Unity.DataBind.Core.Data;

public class ItemStyleData : Context
{
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

	private readonly Property<string> textCompleteStatsProperty = new Property<string>();
	public string TextCompleteStats
	{
		get { return this.textCompleteStatsProperty.Value; }
		set { this.textCompleteStatsProperty.Value = value; }
	}

	public float PositionPercantage;
	public StyleId StyleId;

	public ItemStyleData()
	{
		ItemScale = Vector3.one * 0.6f;
	}

	public void OnClickItem(StyleId styleId)
	{
		Debug.LogErrorFormat ("OnClick styleid: {0}", styleId);
	}

}

