using UnityEngine;
using System.Collections;
using Slash.Unity.DataBind.Core.Data;

public class MainMenuContext : BaseContext
{
	public override ContextType ContextType
	{
		get { return ContextType.MainMenuContext; }
	}

	private readonly Collection<ItemStyleData> itemsStyle = new Collection<ItemStyleData>();
	public Collection<ItemStyleData> ItemsStyle
	{
		get
		{ return this.itemsStyle; }
	}
}

