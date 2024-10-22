﻿using System;
using UnityEngine;
using Z3.GMTK2024.BattleSystem;
using Z3.NodeGraph.Core;

namespace Z3.GMTK2024.Data
{
    [CreateAssetMenu(menuName = GraphPath.Graph + "Samples/" + nameof(CharacterData),
        fileName = "New" + nameof(CharacterData))]
    public class CharacterData : ScriptableObject
    {
        [Header("General")] 
        [SerializeField] private float groundGravity = 3f;
        [SerializeField] private float mass = 1f;

        [Header(" - Movement")] 
        [SerializeField] private float walkMoveSpeed = 2f;

        [SerializeField] private float runMoveSpeed = 6f;
        [SerializeField] private float acceleration = 20f;
        [SerializeField] private float deceleration = 15f;

        [Header(" - Dodge")] 
        [SerializeField] private float dashCooldown = 0.7f;
        [SerializeField] private float dashDuration = 0.7f;
        [SerializeField] private float dashSpeed = 3f;
        [SerializeField] private ParticleSystem.MinMaxCurve dashSpeedVariation;

        [Header(" - Rotation")] 
        [SerializeField] private float mouseSensitivity = 0.2f;

        [SerializeField] private float rotationSmoothTime = 0.12f;
        [SerializeField] private Vector2 cameraRangeRotation = new Vector2(-35f, 75f);

        [Header(" - Jump")] 
        [SerializeField] private float jumpVelocity = 1f;
        [SerializeField] private float jumpStopVelocity = 0.15f;
        [SerializeField] private float jumpGravity = 1f;
        [SerializeField] private Vector2 jumpRangeDuration = new Vector2(-0.04f, 0.25f);

        [Header(" - Air")] 
        [SerializeField] private float fallingGravity = 3f;
        [SerializeField] private float maxFallingVelocity = 11f;

        [Header(" - Attack")] 
        [SerializeField] private float[] attackTimeFrames;

        [Header(" - Size")] 
        [SerializeField] private SizeData sizeData;

        [Header(" - Status")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float invisibleDuration = 2f;

        [SerializeField] private DamageData firstAttackDamage;
        [SerializeField] private DamageData secondAttackDamage;
        [SerializeField] private DamageData thirdAttackDamage;

        // General
        public float GroundGravity => groundGravity; // Map
        public float Mass => mass;

        // Movement
        public float WalkMoveSpeed => walkMoveSpeed; // Map
        public float RunMoveSpeed => runMoveSpeed; // Map
        public float Acceleration => acceleration;
        public float Deceleration => deceleration;

        // Dodge
        public float DashCooldown => dashCooldown;
        public float DashDuration => dashDuration;

        public float DashSpeed => dashSpeed;
        public ParticleSystem.MinMaxCurve DashSpeedVariation => dashSpeedVariation;

        // Rotation
        public Vector2 CameraRangeRotation => cameraRangeRotation;
        public float RotationSmoothTime => rotationSmoothTime;
        public float MouseSensitivity => mouseSensitivity;

        // Jump
        public float JumpGravity => jumpGravity;
        public float JumpVelocity => jumpVelocity;
        public float JumpStopVelocity => jumpStopVelocity;
        public Vector2 JumpRangeDuration => jumpRangeDuration;

        // Air
        public float FallingGravity => fallingGravity;
        public float MaxFallingVelocity => maxFallingVelocity;

        // Attack 
        public float[] AttackTimeFrames => attackTimeFrames;

        // Size
        public SizeData SizeData => sizeData;

        // Status
        public int MaxHealth => maxHealth;
        public float InvisibleDuration => invisibleDuration;
        public DamageData FirstAttackDamage => firstAttackDamage;
        public DamageData SecondAttackDamage => secondAttackDamage;
        public DamageData ThirdAttackDamage => thirdAttackDamage;

    }

    [Serializable]
    public class SizeData
    {
        [field: SerializeField] public float SizeSpeed { get; set; } = 0.5f;
        [field: SerializeField] public float SizeRatio { get; set; } = 1;
        [field: SerializeField] public Vector2 SizeRange { get; set; } = new Vector2(0.5f, 2);
        [field: SerializeField] public float MovementSizeMultiplier { get; set; } = 1;
        [field: SerializeField] public float DamageSizeMultiplier { get; set; } = 1;
        [field: SerializeField] public float CameraDistanceMultiplier { get; set; } = 1;
        [field: SerializeField] public float AttackTimeFrameMultiplier { get; set; } = 1;
        [field: SerializeField] public float DashCooldownMultiplier { get; set; } = 1;
    }
}