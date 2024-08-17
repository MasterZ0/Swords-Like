using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Z3.UIBuilder.Editor;

namespace Z3.DemoSkull.Editor
{
    public class DevelopmentToolsWindow : Z3EditorWindow
    {
        // See this clicking in cs file
        [SerializeField] private VisualTreeAsset visualTree;

        private const string DevelopmentTools = "Development Tools";

        [MenuItem(UiBuildDemoPath.DemoPath + "/" + DevelopmentTools)]
        public static void OpenWindow()
        {
            GetWindow<DevelopmentToolsWindow>(DevelopmentTools).Show();
        }

        protected override void CreateGUI()
        {
            base.CreateGUI();
            PropertyBuilder.CreateInstance<LevelDesignTools>(rootVisualElement);
        }
    }
}