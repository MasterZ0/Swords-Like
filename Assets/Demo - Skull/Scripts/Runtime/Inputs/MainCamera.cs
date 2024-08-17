﻿using Z3.DemoSkull.Shared;
using UnityEngine;

namespace Z3.DemoSkull.Gameplay
{
    public class MainCamera : MonoBehaviour 
    {
        [SerializeField] private Camera mainCamera;

        public static MainCamera Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Reset() => TryGetComponent(out mainCamera);

        public static Vector3 Position => Instance.transform.position;
        public static Camera Camera => Instance.mainCamera;
    }
}