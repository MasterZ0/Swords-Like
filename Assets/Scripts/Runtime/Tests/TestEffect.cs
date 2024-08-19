using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Z3.ObjectPooling;
using Z3.UIBuilder.Core;

namespace Z3.GMTK2024
{
    public class TestEffect : MonoBehaviour
    {
        [SerializeField] private Transform prefab;

        [Button] 
        private void SpawnEffect()
        {
            ObjectPool.SpawnPooledObject(prefab);
        }
    }
}
