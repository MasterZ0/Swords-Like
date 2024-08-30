using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Z3.Audio.FMODIntegration;
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

        [Header("Music")]
        [SerializeField] private SoundReference mainMenu;
        [SerializeField] private SoundReference gameplay;
        [SerializeField] private SoundReference introBoss;
        [SerializeField] private SoundReference bossFight;
        [SerializeField] private SoundReference defeatBoss;

        [Header("Dependencies")]
        [SerializeField] private Enemy boss;
        [SerializeField] private CharacterPawn player;
        [SerializeField] private Transform checkpoint;
        [SerializeField] private Transform playerFightPoint;

        private static bool playerArrivedToBoss;
        private bool cursorLocked;

        private void Awake()
        {
            player.Status.OnDeath += OnPlayerDeath;
            boss.Status.OnDeath += OnBossDefeated;
            fightTrigger.OnTriggerEnterEvent += OnBossTriggered;

            if (!playerArrivedToBoss)
            {
                LockCursor(false);
                AudioManager.SetCurrentMusic(mainMenu);

                menuUI.Init(this);
            }
            else
            {
                LockCursor(true);
                AudioManager.SetCurrentMusic(gameplay);

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

            LockCursor(true);
            AudioManager.SetCurrentMusic(gameplay);
        }

        [Button]
        private void OnBossTriggered(Collider _)
        {
            fightTrigger.OnTriggerEnterEvent -= OnBossTriggered;
            playerArrivedToBoss = true;

            bossTimeline.Play();
            bossTimeline.stopped += StopTimeline;

            player.SetPlayerPosition(playerFightPoint.position, playerFightPoint.rotation);
            AudioManager.SetCurrentMusic(introBoss);

            void StopTimeline(PlayableDirector _)
            {
                bossTimeline.stopped -= StopTimeline;
                AudioManager.SetCurrentMusic(bossFight);
            }
        }

        [Button]
        private void OnBossDefeated()
        {
            AudioManager.SetCurrentMusic(defeatBoss);

            boss.Status.OnDeath -= OnBossDefeated;

            bossDefeatedTimeline.Play();
            bossDefeatedTimeline.stopped += StopTimeline;

            ObjectPool.ReturnAllToPool();

            void StopTimeline(PlayableDirector _)
            {
                bossDefeatedTimeline.stopped -= StopTimeline;
                victoryScreen.SetActive(true);

                LockCursor(false);
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

        private void LockCursor(bool locked)
        {
            cursorLocked = locked;

            if (locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                LockCursor(cursorLocked);
            }
        }
    }
}