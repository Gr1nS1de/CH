namespace Slash.Unity.DataBind.UI.Unity.Setters
{
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;

    /// <summary>
    ///   Sets if a canvas group is interactable depending on a boolean data value.
    ///   <para>Input: Boolean</para>
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] Canvas Group Alpha Setter (Unity)")]
	public class CanvasGroupAlphaSetter : ComponentSingleSetter<CanvasGroup, float>
    {
        #region Methods

		protected override void OnValueChanged(float newValue)
        {
			this.Target.alpha = newValue;
        }

        #endregion
    }
}