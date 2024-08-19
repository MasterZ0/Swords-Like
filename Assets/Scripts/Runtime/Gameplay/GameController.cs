using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Z3.UIBuilder.Core;

namespace Z3.GMTK2024
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector introTimeline;
        [SerializeField] private PlayableDirector bossTimeline;
        [SerializeField] private PlayableDirector bossDefeatedTimeline;

        [Button]
        public void OnIntro()
        {
            introTimeline.gameObject.SetActive(true);
            introTimeline.Play();
            StartCoroutine(StopTimeline());

            IEnumerator StopTimeline()
            {
                yield return new WaitForSeconds((float) introTimeline.duration);
                introTimeline.gameObject.SetActive(false);
            }
        }

        [Button]
        public void OnBossTriggered()
        {
            bossTimeline.gameObject.SetActive(true);
            bossTimeline.Play();
            StartCoroutine(StopTimeline());

            IEnumerator StopTimeline()
            {
                yield return new WaitForSeconds((float) bossTimeline.duration);
                bossTimeline.gameObject.SetActive(false);
            }
        }

        [Button]
        public void OnBossDefeated()
        {
            bossDefeatedTimeline.gameObject.SetActive(true);
            bossDefeatedTimeline.Play();
            // StartCoroutine(StopTimeline());

            IEnumerator StopTimeline()
            {
                yield return new WaitForSeconds((float) bossDefeatedTimeline.duration);
                bossDefeatedTimeline.gameObject.SetActive(false);
            }
        }
    }
}