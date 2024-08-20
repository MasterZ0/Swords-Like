using UnityEngine;
using UnityEngine.UI;

namespace Z3.GMTK2024
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private Button btnPlay;
        [SerializeField] private Button btnQuit;

        private GameController controller;

        internal void Init(GameController gameController)
        {
            controller = gameController;
        }

        private void Awake()
        {
            btnPlay.onClick.AddListener(Play);
            btnQuit.onClick.AddListener(Quit);
        }

        private void Play()
        {
            controller.StartGame();
            gameObject.SetActive(false);
        }

        private void Quit()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}