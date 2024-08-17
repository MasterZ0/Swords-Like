using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Z3.UIBuilder;
using Z3.UIBuilder.Core;
using Z3.UIBuilder.Editor;
using Z3.UIBuilder.TreeViewer;
using Z3.UIBuilder.Editor.TreeViewer;
using Z3.DemoSkull.Data;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.Editor
{
    public class GameDesignWindow : ObjectMenuWindow<ScriptableObject>
    {
        private const string GameDesign = "Game Design";

        [MenuItem(UiBuildDemoPath.DemoPath + "/" + GameDesign)]
        public static void ShowWindow()
        {
            GetWindow<GameDesignWindow>(GameDesign);
        }

        [OnOpenAsset]
        public static bool OpenEditor(int instanceId, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceId) is GameData)
            {
                ShowWindow();
                return true;
            }
            return false;
        }

        protected override void BuildMenuTree(TreeMenu<ScriptableObject> tree)
        {
            GameData gameData = AssetDatabase.LoadAssetAtPath<GameData>(MenuPath.GameDataAsset);

            // TODO: Update extensions methods and don't use T
            //tree.Add("Game Data/Enemies", new LevelDesignTools());

            tree.AddGameData("Game Data", gameData);

            tree.AddAllAssetsAtPath($"Game Data/Enemies", $"{MenuPath.GameConfigFolder}/Enemies", typeof(ScriptableObject), true, IconType.Eye);
            tree.AddAllAssetsAtPath($"Game Data/Items", $"{MenuPath.GameConfigFolder}/Items", typeof(ScriptableObject), true, IconType.Gamepad);
        }

        protected override void OnChangeSelection(ScriptableObject selectedObject)
        {
            // TODO: If is Scriptable Object, show ping
        }

        [UIElement("ping-object")]
        private void OnPingObject() => EditorGUIUtility.PingObject(SelectedObject);
    }
}