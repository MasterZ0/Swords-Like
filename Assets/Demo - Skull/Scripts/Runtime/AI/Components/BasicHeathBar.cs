using Z3.DemoSkull.BattleSystem;
using Z3.DemoSkull.Data;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Z3.DemoSkull.AI
{
    public class BasicHeathBar : MonoBehaviour
    {
        [SerializeField] private Enemy statusOwner; // IStatusOwner
        [SerializeField] private GameObject bar;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider damageDealtBar;

        private GeneralSettings Settings => GameData.General;

        private Coroutine coroutine;
        private float previousHeathPercentage;

        private IAttributes Attributes => statusOwner.GetAttributes();

        private float CurrentHeathPercentage => Attributes.HPPercentage();

        private void OnEnable()
        {
            statusOwner.Status.OnTakeDamage += OnTakeDamage;
            bar.SetActive(false);
        }

        private void OnDisable()
        {
            statusOwner.Status.OnTakeDamage -= OnTakeDamage;
        }

        private void Update()
        {
            if (previousHeathPercentage > CurrentHeathPercentage)
            {
                previousHeathPercentage -= Settings.HeathBarReductionDamageDealt * Time.deltaTime;
                damageDealtBar.value = previousHeathPercentage;

                if (previousHeathPercentage <= CurrentHeathPercentage)
                {
                    coroutine = StartCoroutine(ShowHeathBar());
                }
            }
        }

        private void OnTakeDamage(DamageInfo damageInfo)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            float damageDealtPercentage = (float)damageInfo.EffectiveDamage / Attributes.MaxHP;
            previousHeathPercentage = CurrentHeathPercentage + damageDealtPercentage;

            damageDealtBar.value = previousHeathPercentage;
            healthBar.value = CurrentHeathPercentage;

            bar.SetActive(true);
        }

        private IEnumerator ShowHeathBar()
        {
            yield return new WaitForSeconds(Settings.HeathBarLifetime);
            bar.SetActive(false);
        }
    }
}
