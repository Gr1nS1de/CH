namespace Slash.Unity.DataBind.Foundation.Setters
{
	using UnityEngine;

	/// <summary>
	///   Sets the local scale of a game object depending on a Vector3 data value.
	/// </summary>
	[AddComponentMenu("Data Bind/Foundation/Setters/[DB] Local Scale Setter")]
	public class LocalScaleSetter : GameObjectSingleSetter<Vector3>
	{
		/// <summary>
		///   Called when the data binding value changed.
		/// </summary>
		/// <param name="newValue">New data value.</param>
		protected override void OnValueChanged(Vector3 newValue)
		{
			this.Target.transform.localScale = newValue;
		}
	}
}