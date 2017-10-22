using UnityEngine;
using System.Collections;
using Slash.Unity.DataBind.Core.Data;

public class MainMenuContext : BaseContext
{
	public override ContextType ContextType
	{
		get { return ContextType.MainMenuContext; }
	}


	private readonly Collection<ItemStyleContextData> itemsStyle = new Collection<ItemStyleContextData>();
	public Collection<ItemStyleContextData> ItemsStyle
	{
		get
		{ return this.itemsStyle; }
	}
}

