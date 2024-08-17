using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.UIBuilder;
using Z3.UIBuilder.Core;

namespace Z3.DemoSkull.Data
{
    /// <summary> Game Design Values </summary>
    [EditorIcon(IconType.AudioMixerController)]
    [CreateAssetMenu(menuName = MenuPath.Data + "Game", fileName = "New" + nameof(GameData))]
    public class GameData : ScriptableObject
    {
        [Title("Game Data")]
        [SerializeField] private PlayerSettings player;
        [SerializeField] private GeneralSettings general;
        [SerializeField] private CharacterData character;

        public static PlayerSettings Player => Instance.player;
        public static GeneralSettings General => Instance.general;

        public static GameData Instance { get; private set; }

        private void OnValidate() => Initialize();

        public void Initialize()
        {
            Instance = this;
        }
    }
}