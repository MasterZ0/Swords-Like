using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z3.GMTK2024.BattleSystem;

namespace Z3.GMTK2024
{
    public class RadiusAttackManager : MonoBehaviour
    {
        [SerializeField] private HitBox smallHb;
        [SerializeField] private HitBox mediumHb;
        [SerializeField] private HitBox largeHb;

        private void Awake()
        {
            smallHb = transform.Find("SmallHitbox").GetComponent<HitBox>();
            mediumHb = transform.Find("MediumHitbox").GetComponent<HitBox>();
            largeHb = transform.Find("LargeHitbox").GetComponent<HitBox>();
        }

        public void SetDamage(Damage small, Damage medium, Damage large)
        {

        }
    }
}
