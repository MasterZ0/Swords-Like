using Z3.UIBuilder.Core;
using UnityEngine;

namespace Z3.DemoSkull.Data
{
    [System.Serializable]
    public class PlayerVisualSettings
    {
        //[Title(VFX)]
        //[SerializeField] private TrailData trailData = new TrailData(0.3f, 0.03f);

        [Title(Animations), Range(0f, 20f)]
        [SerializeField] private float hardFallingVelocity = 10f;
        [SerializeField] private float runningStopTime = 0.5f;

        [Title(InjuryFX)]
        [SerializeField]
        private Gradient bloodColor = new Gradient()
        {
            colorKeys = new GradientColorKey[1] { new GradientColorKey(Color.red, 0) }
        };
        [Range(0.01f, 2f)]
        [SerializeField] private float injuryRedColorDuration = .7f;
        [Range(0.01f, 1f)]
        [SerializeField] private float visibleInjuredTime = .1f;
        [Range(0.01f, 1f)]
        [SerializeField] private float invisibleInjuredTime = .1f;
        [Range(0f, 1f)]
        [SerializeField] private float alphaInvisible = .35f;

        [Title(UI)]
        [Range(0f, 100f)]
        [SerializeField] private float lowHealthPercentage = 20f;
        [SerializeField] private float timeToHideStamina = 2.5f;
        [SerializeField] private float timeToShowGameOver = 2f;

        [Title(Camera)]
        [Range(-5f, 0f)]
        [SerializeField] private float downViewDistance = -2f;
        [Range(0f, 20f)]
        [SerializeField] private float downViewCameraSpeed = 8f;

        //public TrailData TrailData => trailData;
        public float HardFallingVelocity => hardFallingVelocity;
        public float RunningStopTime => runningStopTime;

        public Gradient BloodColor => bloodColor;
        public float InjuryRedColorDuration => injuryRedColorDuration;
        public float VisibleInjuredTime => visibleInjuredTime;
        public float InvisibleInjuredTime => invisibleInjuredTime;
        public float AlphaInvisible => alphaInvisible;

        public float LowHealthPercentage => lowHealthPercentage;
        public float TimeToHideStamina => timeToHideStamina;
        public float TimeToShowGameOver => timeToShowGameOver;

        public float DownViewDistance => downViewDistance;
        public float DownViewCameraSpeed => downViewCameraSpeed;


        private const string CharacterCustomization = "Character Customization";
        private const string VFX = "VFX";
        private const string InjuryFX = "InjuryFX";
        private const string Animations = "Animations";
        private const string UI = "UI";
        private const string Camera = "Camera";
    }
}