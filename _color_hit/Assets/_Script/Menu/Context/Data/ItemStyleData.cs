using UnityEngine;
using System.Collections;
using Slash.Unity.DataBind.Core.Data;

public class ItemStyleData : Context
{
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
}

