namespace Slash.Unity.DataBind.Foundation.Setters
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	///   Setter which activates/deactivates a game object depending on the boolean data value.
	/// </summary>
	[AddComponentMenu("Data Bind/Foundation/Setters/[DB] Scroll Rect Position Setter")]
	public class ScrollRectPositionSetter : ComponentSingleSetter<ScrollRect, float>
	{

		#region Methods

		protected override void OnValueChanged(float newValue)
		{
			this.Target.verticalNormalizedPosition = newValue;
		}
			

		#endregion


	}
}