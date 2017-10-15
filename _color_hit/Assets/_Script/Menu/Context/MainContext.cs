using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slash.Unity.DataBind.Core.Data;

public class MainContext : Context 
{
	public MainMenuContext MainMenuContext {get; set;}
	public SelectLevelContext SelectLevelContext {get; set;}
	
	public MainContext()
	{
		MainMenuContext = new MainMenuContext ();
		SelectLevelContext = new SelectLevelContext ();
	}
}
