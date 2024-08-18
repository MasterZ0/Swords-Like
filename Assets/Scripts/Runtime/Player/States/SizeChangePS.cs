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

        private Vector3? initialSize;
        private Dictionary<CinemachineVirtualCameraBase, float> cameraDistances = new();


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

            initialSize ??= Physics.Transform.localScale;

            // Apply the size effects
            Physics.Transform.localScale = initialSize.Value * (1 + sizeRatio);
            Physics.MovementScale = 1 + sizeRatio * Data.SizeData.MovementSizeMultiplier;
            UpdateCameraDistance(1 + sizeRatio);
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