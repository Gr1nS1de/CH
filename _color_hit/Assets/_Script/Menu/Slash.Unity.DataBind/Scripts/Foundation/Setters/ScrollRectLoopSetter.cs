namespace Slash.Unity.DataBind.Foundation.Setters
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	///  
	/// </summary>
	[AddComponentMenu("Data Bind/Foundation/Setters/[DB] Scroll Rect Loop Setter")]
	public class ScrollRectLoopSetter : GameObjectSingleSetter<bool>
	{
		private bool _scrollingFlag = true;
		ScrollRect scrollRect;
		RectTransform scrollRectTransform;
		RectTransform scrollRectItemContainer;

		#region Methods

		protected override void OnValueChanged(bool newValue)
		{
			_scrollingFlag = newValue;

			if (newValue)
			{
				scrollRect = this.Target.GetComponent<ScrollRect> ();
				scrollRectItemContainer = scrollRect.content;
				scrollRectTransform = this.Target.GetComponent<RectTransform> ();

				//Start from beginning
				scrollRectItemContainer.anchoredPosition = Vector3.zero;
			}
		}

		void Update()
		{
			if (_scrollingFlag)
				Movement ();
		}

		private void  Movement()
		{
			float top =  scrollRectItemContainer.offsetMax.y;            				
			float bottom = scrollRectItemContainer.offsetMin.y;   
			float itemsContainerHeight = scrollRect.content.rect.height;

			scrollRectItemContainer.anchoredPosition = Vector2.Lerp(scrollRectItemContainer.anchoredPosition, scrollRectItemContainer.anchoredPosition + Vector2.up, Time.deltaTime * 600f);

			if(bottom  > Mathf.Abs( scrollRectTransform.offsetMin.y) - Mathf.Abs(scrollRectTransform.offsetMax.y))
			{
				RectTransform rt = scrollRect.content;     
				Vector2 v = rt.anchoredPosition;           
				v.y = 0;
				rt.anchoredPosition = v;
			}
			else if(top < 0f)
			{
				RectTransform rt = scrollRect.content;
				Vector2 v = rt.anchoredPosition;
				v.y = itemsContainerHeight * 1.5f;
				rt.anchoredPosition = v;
			}
		}

		#endregion


	}
}