using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slash.Unity.DataBind.Core.Data;

public class SelectLevelContext : BaseContext 
{
	public override ContextType ContextType
	{
		get { return ContextType.SelectLevelContext; }
	}

	private readonly Property<string> completedLevelsTextProperty = new Property<string>();
	public string CompletedLevelsText
	{
		get { return this.completedLevelsTextProperty.Value; }
		set { this.completedLevelsTextProperty.Value = value; }
	}

	private readonly Collection<ItemSelectContextData> itemsSelect = new Collection<ItemSelectContextData>();
	public Collection<ItemSelectContextData> ItemsSelect
	{
		get
		{ return this.itemsSelect; }
	}
}
