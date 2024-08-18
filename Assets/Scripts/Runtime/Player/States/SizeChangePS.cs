using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Z3.GMTK2024.States;
using Z3.NodeGraph.Core;

namespace Z3.GMTK2024
{
    public class SizeChangePS : CharacterAction
    {
        [SerializeField] private Parameter<float> sizeRatio;

        private bool isInitialized;
        private float initialSizeRatio;
        private Dictionary<CinemachineVirtualCameraBase, float> cameraDistances = new();

        protected override void StartAction()
        {
            base.StartAction();
            if (isInitialized)
                return;

            isInitialized = true;
            var initialSize = Physics.Transform.localScale.x;
            var sizeRange = Data.SizeData.SizeRange;
            sizeRatio = (initialSize - sizeRange.x) / (sizeRange.y - sizeRange.x);
            initialSizeRatio = sizeRatio;
            Apply();
        }

        protected override void UpdateAction()
        {
            base.UpdateAction();
            float multiplier = 0;
            if (Controller.IsSizeIncreasedPressed)
            {
                multiplier = 1;
            }
            else if (Controller.IsSizeDecreasePressed)
            {
                multiplier = -1;
            }

            sizeRatio += Data.SizeData.SizeSpeed * Time.deltaTime * multiplier;
            sizeRatio = Mathf.Clamp01(sizeRatio);


            // Apply the size effects
            Apply();
        }

        private void Apply()
        {
            var sizeRange = Data.SizeData.SizeRange;
            float size = (sizeRange.y - sizeRange.x) * (sizeRatio) + sizeRange.x;
            Physics.Transform.localScale = Vector3.one * size;

            // The starting size might not be from sizeRange.x, and initially we need everything to match with what
            // we set up in the Data.
            // Therefore, I am subtracting the initialSizeRatio to make it starts from zero.
            float realSizeRatio = sizeRatio - initialSizeRatio;
            Physics.MovementScale = 1 + realSizeRatio * Data.SizeData.MovementSizeMultiplier;
            UpdateCameraDistance(1 + realSizeRatio);
        }

        private void UpdateCameraDistance(float realSizeRatio)
        {
            var camera = FindActiveCamera();
            var componentBase = camera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (componentBase is Cinemachine3rdPersonFollow cinemachine3RdPersonFollow)
            {
                float initialCameraDistance = 1;
                if (cameraDistances.TryGetValue(camera, out float distance))
                {
                    initialCameraDistance = distance;
                }
                else
                {
                    initialCameraDistance = cinemachine3RdPersonFollow.CameraDistance;
                    cameraDistances.Add(camera, initialCameraDistance);
                }

                cinemachine3RdPersonFollow.CameraDistance = initialCameraDistance * realSizeRatio;
            }
            else
            {
                throw new NotImplementedException($"Camera distance has not been implemented for {componentBase}");
            }

            return;

            CinemachineVirtualCamera FindActiveCamera()
            {
                foreach (var camera in Camera.CinemachineVirtualCameras)
                {
                    if (camera.isActiveAndEnabled)
                    {
                        return camera;
                    }
                }

                throw new Exception("No active camera");
            }
        }
    }
}