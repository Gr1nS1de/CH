// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageTextureSetter.cs" company="Slash Games" & Staya>
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.UI.Unity.Setters
{
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    ///   Setter which sets the Texture2D value of an image component.
    ///   <para>Input: <see cref="Texture2D"/></para>
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] Image Sprite Setter (Unity)")]
    public class ImageTextureSetter : ComponentSingleSetter<RawImage, Texture2D>
    {
        #region Methods

        protected override void OnValueChanged(Texture2D newValue)
        {
            this.Target.texture = newValue;
        }

        #endregion
    }
}
