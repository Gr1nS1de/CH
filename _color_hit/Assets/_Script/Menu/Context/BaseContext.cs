using Slash.Unity.DataBind.Core.Data;
using UnityEngine;

public enum ContextType
{
	none,
	MainMenuContext,
	SelectLevelContext
}

public abstract class BaseContext : Context 
{
	public abstract ContextType ContextType { get;}

	private readonly Property<bool> isShowProperty = new Property<bool>();
	public bool IsShow
	{
		get { return this.isShowProperty.Value; }
		set { this.isShowProperty.Value = value; }
	}

	public virtual void Load()
	{
		Debug.LogErrorFormat ("Load state: {0}", ContextType);
		IsShow = true;
	}

	public virtual void UnLoad()
	{
		IsShow = false;
	}
}
