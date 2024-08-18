using System;
using UnityEngine;

namespace Z3.GMTK2024
{
    [Serializable]
    public sealed class CharacterUI
    {
        [field: SerializeField] public SizeChangeUI SizeChangeUI { get; private set; }
    }
}