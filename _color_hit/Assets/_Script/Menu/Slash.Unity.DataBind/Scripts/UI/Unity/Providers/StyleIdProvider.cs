namespace Slash.Unity.DataBind.Foundation.Providers.Getters
{
	using System;

	using Slash.Unity.DataBind.Core.Presentation;
	using Slash.Unity.DataBind.Core.Utils;

	using UnityEngine;

	/// <summary>
	///   Provides the enum values of a specified enum type.
	/// </summary>
	public class StyleIdProvider :  DataProvider
	{
		#region Fields

		public StyleId StyleId;

		#endregion

		#region Properties


		/// <summary>
		///   Current data value.
		/// </summary>
		public override object Value
		{
			get
			{
				return StyleId;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///   Called when the value of the data provider should be updated.
		/// </summary>
		protected override void UpdateValue()
		{
		}

		#endregion
	}
}
