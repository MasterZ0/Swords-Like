﻿using Z3.GMTK2024.Gameplay;
using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Z3.GMTK2024
{
    [Serializable]
    public class CharacterCamera : CharacterControllerComponent
    {
        [Header("Camera")] [SerializeField] private Transform cameraTarget;

        [SerializeField] private CinemachineVirtualCamera[] cinemachineVirtualCameras;

        public bool XLocked { get; private set; }
        public bool YLocked { get; private set; }

        private float xRotation;
        private float yRotation;

        private const float Threshold = 0.01f;
        public Transform CameraTarget => MainCamera.Camera.transform;

        public CinemachineVirtualCamera[] CinemachineVirtualCameras => cinemachineVirtualCameras;

        private GameObject currentCamera;

        #region Initialization

        public override void Init(CharacterPawn CharacterController)
        {
            base.Init(CharacterController);

            yRotation = cameraTarget.eulerAngles.y;
        }

        #endregion

        public void Update()
        {
            UpdateCameraRotation();
        }

        private void UpdateCameraRotation()
        {
            Vector2 look = Controller.Controller.Look;

            if (look.sqrMagnitude >= Threshold)
            {
                if (!XLocked)
                {
                    xRotation -= look.y * Data.MouseSensitivity;
                    xRotation = Mathf.Clamp(xRotation, Data.CameraRangeRotation.x, Data.CameraRangeRotation.y);
                }

                if (!YLocked)
                {
                    yRotation += look.x * Data.MouseSensitivity;
                    yRotation = ClampAngle(yRotation);
                }
                else
                {
                    yRotation = cameraTarget.eulerAngles.y;
                }
            }

            cameraTarget.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }

        private static float ClampAngle(float angle)
        {
            if (angle < -360f)
                angle += 360f;

            if (angle > 360f)
                angle -= 360f;

            return angle;
        }

        internal void SwitchCamera(GameObject newCamera)
        {
            if (currentCamera)
            {
                currentCamera.SetActive(false);
            }

            currentCamera = newCamera;
            currentCamera.SetActive(true);
        }

        internal void LockY(bool locked) => YLocked = locked;
    }
}