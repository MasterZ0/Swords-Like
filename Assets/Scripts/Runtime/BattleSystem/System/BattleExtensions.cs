using UnityEngine;

namespace Z3.GMTK2024.BattleSystem
{
    /// <summary>
    /// Extensions to make interfaces easier to use <see cref="IStatusOwner"/>, <see cref="IStatusOwner"/> and <see cref="IAttributes"/>
    /// </summary>
    public static class BattleExtensions
    {
        #region Deal Damage
        public static void Kill(this IStatusOwner controller)
        {
            int hp = controller.GetAttributes().CurrentHP;
            controller.TakeDamage(hp);
        }

        /// <param name="damagePercentage"> 0 - 100 </param>
        public static void TakeDamagePercentage(this IStatusOwner controller, float damagePercentage)
        {
            int hp = controller.GetAttributes().CurrentHP;
            int damageValue = Mathf.RoundToInt(hp * damagePercentage * 0.01f);
            controller.TakeDamage(damageValue);
        }

        public static void TakeDamage(this IStatusOwner hittable, int value)
        {
            Damage damage = new Damage(value);
            hittable.TakeDamage(damage);
        }
        #endregion

        #region IStatusOwner simplified
        public static void TakeDamage(this IStatusOwner controller, Damage damage) => controller.Status.TakeDamage(damage);
        public static IAttributes GetAttributes(this IStatusOwner controller) => controller.Status.Attributes;
        public static bool IsDead(this IStatusOwner controller) => controller.GetAttributes().IsDead();
        #endregion

        #region IStatusController simplified
        public static bool IsDead(this IStatusController status)
        {
            return status.Attributes.IsDead();
        }
        #endregion

        #region Attributes
        public static float HPPercentage(this IAttributes attributes) => (float)attributes.CurrentHP / attributes.MaxHP;
        public static bool IsDead(this IAttributes attributes) => attributes.CurrentHP <= 0;
        #endregion
    }
}