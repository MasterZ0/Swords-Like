using Z3.UIBuilder.Core;
using UnityEngine;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.Data
{
    [CreateAssetMenu(menuName = MenuPath.SettingsGlobal + nameof(GeneralSettings), fileName = nameof(GeneralSettings))]
    public class GeneralSettings : ScriptableObject
    {
        [Title("Platform and Tools")]
        [SerializeField] private bool profiler = true;
        [SerializeField] private bool hideUI;
        [SerializeField] private bool godMode;

        [Title("VFX")]
        [SerializeField] private float hitMaterialSeconds = 0.1f;
        [SerializeField] private Material hitMaterial; // VFX
        [SerializeField] private float heathBarLifetime = 1f;
        [SerializeField] private float heathBarReductionDamageDealt = 1f;

        [Title("Items")]
        [MinMaxSlider(0f, 30f)]
        [SerializeField] private Vector2 dropItemYVelocityRange = new Vector2(0, 5);
        [Range(0f, 30f)]
        [SerializeField] private float dropItemZXMaxVelocity = 2;
        [Space]
        [SerializeField] private int dropRestoreHP = 10;

        [Title("Physics")]
        [Range(0, 2f)]
        [SerializeField] private float strongKnockback = 0.4f;
        [Range(0, 2f)]
        [SerializeField] private float mediumKnockback = 0.15f;
        [Range(0, 2f)]
        [SerializeField] private float weakKnockback = 0.05f;

        public float HitMaterialSeconds => hitMaterialSeconds;
        public Material HitMaterial => hitMaterial;
        
        public float StrongKnockback => strongKnockback;
        public float MediumKnockback => mediumKnockback;
        public float WeakKnockback => weakKnockback;
        public float HeathBarLifetime => heathBarLifetime;
        public float HeathBarReductionDamageDealt => heathBarReductionDamageDealt;

        public Vector2 DropItemYVelocityRange => dropItemYVelocityRange;
        public float DropItemZXMaxVelocity => dropItemZXMaxVelocity;

        public int DropRestoreHP => dropRestoreHP;
        public bool Profiler => profiler;
        public bool HideUI => hideUI;
        public bool GodMode => godMode;
    }
}