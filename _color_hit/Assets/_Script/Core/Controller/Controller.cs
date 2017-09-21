using UnityEngine;
using System.Collections;

public abstract class Controller : Element
{
	public virtual void OnNotification( string alias, Object target, params object[] data ) { }

	public GameApplication game 	{ get { return (GameApplication)m_Game; } }
	public MenuApplication ui			{ get { return (MenuApplication)m_UI; } }
	public Core core				{ get { return (Core)m_Core; } }
}
