using UnityEngine;
using System.Collections;

public abstract class View<T> : View
{
	public GameApplication 	game 	{ get { return (GameApplication)m_Game; } }
	public MenuApplication 	ui		{ get { return (MenuApplication)m_UI; } }
}


public abstract class View : Element
{
	public GameApplication 	game 	{ get { return (GameApplication)m_Game; } }
	public MenuApplication 	ui		{ get { return (MenuApplication)m_UI; } }
	public Core core				{ get { return (Core)m_Core; } }

	public virtual void OnRendererCollisionEnter(ViewCollisionDetect collisionDetector, Collision2D collision)
	{
	}

	public virtual void OnRendererCollisionExit(ViewCollisionDetect collisionDetector, Collision2D collision)
	{
	}

	public virtual void OnRendererTriggerEnter(ViewTriggerDetect triggerDetector, Collider2D otherCollider)
	{
	}

	public virtual void OnRendererTriggerExit(ViewTriggerDetect triggerDetector, Collider2D otherCollider)
	{
	}

	public virtual void OnVisible(ViewVisibleDetect visibleDetector)
	{
	}

	public virtual void OnInvisible(ViewVisibleDetect visibleDetector)
	{
	}
}



