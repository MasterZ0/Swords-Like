using System;
using UnityEngine;
using UnityEngine.UI;

namespace Z3.GMTK2024
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private Button btnPlay;
        [SerializeField] private Button btnQuit;

        private void Awake()
        {
            btnPlay.onClick.AddListener(Play);
            btnQuit.onClick.AddListener(Quit);
        }

        private void Play()
        {
            GameController.Instance.OnIntro();
            transform.root.gameObject.SetActive(false);
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}