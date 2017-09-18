﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphicColorSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.UI.Unity.Setters
{
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    ///   Set the color of a Graphic depending on the color data value.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] Graphic Color Setter (Unity)")]
    public class GraphicColorSetter : ComponentSingleSetter<Graphic, Color>
    {
        #region Methods

        /// <summary>
        ///   Called when the data binding value changed.
        /// </summary>
        /// <param name="newValue">New data value.</param>
        protected override void OnValueChanged(Color newValue)
        {
            this.Target.color = newValue;
        }

        #endregion
    }
}