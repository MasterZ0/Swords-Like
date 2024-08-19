using UnityEngine;
using Z3.GMTK2024.BattleSystem;

namespace Z3.GMTK2024
{
    public class RadiusAttackController : MonoBehaviour
    {
        [SerializeField] private HitBox smallHitbox;
        [SerializeField] private HitBox mediumHitbox;
        [SerializeField] private HitBox largeHitbox;

        public void SetDamage(Damage smallDamage, Damage mediumDamage, Damage largeDamage)
        {
            smallHitbox.SetDamage(smallDamage);
            smallHitbox.SetDamage(mediumDamage);
            smallHitbox.SetDamage(largeDamage);
        }
    }
}
