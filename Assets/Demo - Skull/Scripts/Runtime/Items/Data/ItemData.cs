using UnityEngine;
using Z3.UIBuilder.Core;

namespace Z3.DemoSkull.Items.Data
{
    /// <summary>
    /// Basic information of all items
    /// </summary>
    public abstract class ItemData : ScriptableObject
    {
        [Title("Item Data")]
        public string itemName;
        public string itemDescription;

        //[VerticalGroup("Vertical")]
        //[HorizontalGroup("Vertical/Horizontal")]
        //[BoxGroup("Vertical/Horizontal/Icon", centerLabel: true)]
        //[PreviewField(200, ObjectFieldAlignment.Center), HideLabel]
        public Sprite icon;

        //[BoxGroup("Vertical/Horizontal/Image", centerLabel: true)]
        //[PreviewField(200, ObjectFieldAlignment.Center), HideLabel]
        public Sprite image;

        //[BoxGroup("Vertical/Horizontal/Model", centerLabel: true)]
        //[PreviewField(200, ObjectFieldAlignment.Center), HideLabel]
        public GameObject model;

        public Transform collectFX;
    }
}