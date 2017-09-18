
namespace Slash.Unity.DataBind.Foundation.Setters
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	///   Sets the position of a game object depending on a Vector3 data value.
	/// </summary>
	[AddComponentMenu("Data Bind/Foundation/Setters/[DB] UI Size Setter")]
	public class SizeSetter : GameObjectSingleSetter<bool>
	{
		public Vector2 	ChangeSize;

		private Vector2 _startSize;
		private RectTransform _rectTransform;

		#region Methods

		protected override void OnValueChanged(bool newValue)
		{
			if (_rectTransform == null)
			{
				if (this.Target.GetComponent<RectTransform> ())
				{
					_rectTransform = this.Target.GetComponent<RectTransform> ();
					_startSize = _rectTransform.sizeDelta;
				}

				if (!_rectTransform)
				{
					Debug.LogError ("Rect is null");
					return;
				}
			}

			_rectTransform.sizeDelta = newValue ? ChangeSize : _startSize;
		}



		#endregion
	}
}