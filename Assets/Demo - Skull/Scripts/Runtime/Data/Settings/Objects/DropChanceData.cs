using Z3.UIBuilder.Core;
using UnityEngine;
using System;
using System.Collections.Generic;
using Z3.DemoSkull.Items.Data;

namespace Z3.DemoSkull.Data
{
    [Serializable, InlineProperty, HideLabel]
    public class DropChanceData
    {
        [MinMaxSlider(0, 20)]
        [SerializeField] private Vector2Int goldRange;

        [SerializeField] private List<DropChance<DropItem>> loot;

        public Vector2Int GoldRange => GoldRange;
        public List<DropChance<DropItem>> Loot => loot;
    }

    [Serializable]
    public class DropChance<T>
    {
        public T drop;
        [Range(0, 100)]
        public float chance;
    }

    [Serializable, InlineProperty, HideLabel]
    public class DropItem
    {
        public ItemData item;
        [MinMaxSlider(0, 1000), ShowIf(nameof(ShowAmount))]
        public Vector2Int amountRange;

        private bool ShowAmount => item is IQuantifiable;
    }
}