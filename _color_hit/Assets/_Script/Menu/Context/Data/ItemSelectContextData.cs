using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Slash.Unity.DataBind.Core.Data;
using DG.Tweening;
using System;

public class ItemSelectContextData : Context
{
	public event Action<int, int> ActionClick = delegate {};

	private readonly Property<Color> topColorProperty = new Property<Color>();
	public Color TopColorProperty
	{
		get { return this.topColorProperty.Value; }
		set { this.topColorProperty.Value = value; }
	}

	private readonly Property<Color> bottomColorProperty = new Property<Color>();
	public Color BottomColorProperty
	{
		get { return this.bottomColorProperty.Value; }
		set { this.bottomColorProperty.Value = value; }
	}

	public int Level;

	public ItemSelectContextData()
	{
		TopColorProperty = Color.white;
		BottomColorProperty = Color.white;
	}

	public void ClickStep(bool isTop)
	{
		ActionClick (Level, isTop ? 1 : 0);
	}
}

