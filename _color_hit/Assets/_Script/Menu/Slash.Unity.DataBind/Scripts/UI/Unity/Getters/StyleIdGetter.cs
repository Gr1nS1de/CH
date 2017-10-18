
namespace Slash.Unity.DataBind.UI.Unity.Getters
{
	using Slash.Unity.DataBind.Foundation.Providers.Getters;

	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	///   Gets the value of an style id provider.
	/// </summary>
	[AddComponentMenu("Data Bind/UnityUI/Getters/[DB] Style Id Getter (Unity)")]
	public class StyleIdGetter : ComponentSingleGetter<StyleIdProvider, StyleId>
	{
		#region Methods

		/// <summary>
		///   Register listener at target to be informed if its value changed.
		///   The target is already checked for null reference.
		/// </summary>
		/// <param name="target">Target to add listener to.</param>
		protected override void AddListener(StyleIdProvider target)
		{
		}

		/// <summary>
		///   Derived classes should return the current value to set if this method is called.
		///   The target is already checked for null reference.
		/// </summary>
		/// <param name="target">Target to get value from.</param>
		/// <returns>Current value to set.</returns>
		protected override StyleId GetValue(StyleIdProvider target)
		{
			return target.StyleId;
		}

		/// <summary>
		///   Remove listener from target which was previously added in AddListener.
		///   The target is already checked for null reference.
		/// </summary>
		/// <param name="target">Target to remove listener from.</param>
		protected override void RemoveListener(StyleIdProvider target)
		{
		}

		private void OnTargetValueChanged(float newValue)
		{
			this.OnTargetValueChanged();
		}

		#endregion
	}
}