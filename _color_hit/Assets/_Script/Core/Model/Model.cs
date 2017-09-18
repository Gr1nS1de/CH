using UnityEngine;
using System.Collections;


public abstract class Model : Element
{
	public GameApplication 	game 	{ get { return (GameApplication)m_Game; } }
	public MenuApplication 	ui		{ get { return (MenuApplication)m_UI; } }
}
