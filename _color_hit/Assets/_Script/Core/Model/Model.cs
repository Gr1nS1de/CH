using UnityEngine;
using System.Collections;


public abstract class Model : Element
{
	public GameApplication 	game 	{ get { return (GameApplication)m_Game; } }
	public MenuApplication 	ui		{ get { return (MenuApplication)m_UI; } }
	public Core core				{ get { return (Core)m_Core; } }
}
