using UnityEngine;
using System.Collections;

public class LevelController : Controller
{

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.SetStyle_:
				{
					break;
				}

		}
	}
}

