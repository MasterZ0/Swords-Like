using System;
using UnityEngine;
using UnityEngine.UI;

namespace Z3.GMTK2024
{
    public class ThanksForPlayingUI : MonoBehaviour
    {
        [SerializeField] private Button btnReplay;

        private void Awake()
        {
            btnReplay.onClick.AddListener(Replay);
        }

        private void Replay()
        {
            print("Replay");
        }
    }
}