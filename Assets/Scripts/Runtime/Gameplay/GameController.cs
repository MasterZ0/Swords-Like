using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Z3.GMTK2024.AI;
using Z3.NodeGraph.Core;
using Z3.ObjectPooling;
using Z3.UIBuilder.Core;

namespace Z3.GMTK2024
{
    public class GameController : MonoBehaviour
    {
        [Header("Cutscenes")]
        [SerializeField] private PlayableDirector introTimeline;
        [SerializeField] private PlayableDirector bossTimeline;
        [SerializeField] private PlayableDirector bossDefeatedTimeline;
        [Space]
        [SerializeField] private MonoEventDispatcher fightTrigger;

        [Header("UI")]
        [SerializeField] private MenuUI menuUI;
        [SerializeField] private GameObject deathScreen;
        [Space]
        [SerializeField] private GameObject victoryScreen;

        [Header("Dependencies")]
        [SerializeField] private Enemy boss;
        [SerializeField] private CharacterPawn player;
        [SerializeField] private Transform checkpoint;
        [SerializeField] private Transform playerFightPoint;

        private static bool playerArrivedToBoss;

        private void Awake()
        {
            player.Status.OnDeath += OnPlayerDeath;
            boss.Status.OnDeath += OnBossDefeated;
            fightTrigger.OnTriggerEnterEvent += OnBossTriggered;

            if (!playerArrivedToBoss)
            {
                menuUI.Init(this);
            }
            else
            {
                introTimeline.gameObject.SetActive(false);
                menuUI.gameObject.SetActive(false);

                player.transform.parent.gameObject.SetActive(true);
                player.SetPlayerPosition(checkpoint.position, checkpoint.rotation);
            }
        }

        [Button]
        public void StartGame() // Called after press "Play" Button
        {
            introTimeline.Play();
        }

        [Button]
        private void OnBossTriggered(Collider _)
        {
            fightTrigger.OnTriggerEnterEvent -= OnBossTriggered;
            playerArrivedToBoss = true;

            bossTimeline.Play();

            player.SetPlayerPosition(playerFightPoint.position, playerFightPoint.rotation);
        }

        [Button]
        private void OnBossDefeated()
        {
            boss.Status.OnDeath -= OnBossDefeated;

            bossDefeatedTimeline.Play();
            bossDefeatedTimeline.stopped += StopTimeline;

            ObjectPool.ReturnAllToPool();

            void StopTimeline(PlayableDirector _)
            {
                bossDefeatedTimeline.stopped -= StopTimeline;
                victoryScreen.SetActive(true);
            }
        }

        private void OnPlayerDeath()
        {
            player.Status.OnDeath -= OnPlayerDeath;
            deathScreen.SetActive(true);
        }

        public void OnGameOverEnd()
        {
            ReloadGame();
        }

        public void OnReplay()
        {
            playerArrivedToBoss = false;
            ReloadGame();
        }

        private void ReloadGame()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}