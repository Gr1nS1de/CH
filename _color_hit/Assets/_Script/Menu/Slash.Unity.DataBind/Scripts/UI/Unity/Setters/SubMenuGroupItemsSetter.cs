// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubMenuGroupItemsSetter.cs" company="Slash Games/Staya">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.UI.Unity.Setters
{
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;
    using UnityEngine.UI;
    using System;

    /// <summary>
    ///   Set the items of a LayoutGroup depending on the items of the collection data value.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] SubMenu Group Items Setter (Unity)")]
    public sealed class SubMenuGroupItemsSetter : GameObjectItemsSetter<LayoutGroup>
    {
        protected override void OnItemCreated(object itemContext, GameObject itemObject)
        {
            base.OnItemCreated(itemContext, itemObject);
            itemObject.transform.SetSiblingIndex(Target.transform.childCount - 2);
        }
    }
}

