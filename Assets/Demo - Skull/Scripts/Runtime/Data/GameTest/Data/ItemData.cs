using System.Collections.Generic;
using UnityEngine;
using Z3.DemoSkull.BattleSystem;
using Z3.DemoSkull.Shared;
using Z3.UIBuilder.Core;

namespace Z3.DemoSkull.Data
{
    [CustomIcon(nameof(GetTexture))]
    [CreateAssetMenu(menuName = MenuPath.Data + "Item", fileName = "New" + nameof(ItemData))]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private int value;
        [SerializeField] private Color color;
        [SerializeField] private Texture2D icon;

        [SerializeReference/*, TypeSelection*/] private List<Effect> effects = new List<Effect>();
        [SerializeField, SerializeReference/*, TypeSelection*/] private Effect effect;
        public int Value => value;
        public Color Color => color;

        private Texture2D GetTexture() => icon;
    }
}