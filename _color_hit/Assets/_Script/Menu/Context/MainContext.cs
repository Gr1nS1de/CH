using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slash.Unity.DataBind.Core.Data;

public class MainContext : Context 
{
	private readonly Property<bool> isShowMainMenuProperty = new Property<bool>();
	public bool IsShowMainMenu
	{
		get { return this.isShowMainMenuProperty.Value; }
		set { this.isShowMainMenuProperty.Value = value; }
	}

	private readonly Property<int> resourceStarsCountProperty = new Property<int>();
	public int ResourceStarsCount
	{
		get { return this.resourceStarsCountProperty.Value; }
		set { ResourceStarsCountText = Utils.SweetMoney (value); this.resourceStarsCountProperty.Value = value; }
	}

	private readonly Property<string> resourceStarsCountTextProperty = new Property<string>();
	public string ResourceStarsCountText
	{
		get { return this.resourceStarsCountTextProperty.Value; }
		set { this.resourceStarsCountTextProperty.Value = value; }
	}

	public MainMenuContext MainMenuContext { get; set;}
	public SelectLevelContext SelectLevelContext { get; set;}
	public InGameMenuContext InGameMenuContext { get; set;}
	
	public MainContext()
	{
		MainMenuContext = new MainMenuContext ();
		SelectLevelContext = new SelectLevelContext ();
		InGameMenuContext = new InGameMenuContext ();

		IsShowMainMenu = true;
	}
}
