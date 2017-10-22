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

	private readonly Collection<ItemSelectContextData> itemsSelect = new Collection<ItemSelectContextData>();
	public Collection<ItemSelectContextData> ItemsSelect
	{
		get
		{ return this.itemsSelect; }
	}
}
