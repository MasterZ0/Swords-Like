using Z3.Utils;

namespace Z3.DemoSkull.Shared
{
    public class MenuPath 
    {

        // Assets
        public const string GameDataAsset = GameConfigFolder + "GameConfig.asset";
        public const string GameConfigFolder = "Assets/Plugins/Z3/Demo - Skull/Data/Config/";



        public const string ScriptableObjects = Z3Path.ScriptableObjects + "Demo Skull/";
        public const string Data = ScriptableObjects + "Data/";
        public const string Settings = Data + "Settings/";
        public const string SettingsGlobal = Data + "Global/";
        public const string SettingsSub = Data + "Sub/";
        public const string SettingsEnemies = Data + "Enemies/";
        public const string Items = Data + "Items/";
        public const string SettingsPlayers = Data + "Player/";

        // Categories
        private const string SkullDemo = "Skull Demo/";


        public const string Analyzers = SkullDemo + "/Analyzers";
        public const string Audio = SkullDemo + "/Audio";
        public const string Timeline = SkullDemo + "/Timeline";
        public const string Effects = SkullDemo + "/Effects";
        public const string Events = SkullDemo + "/Events";
        public const string GameManager = SkullDemo + "/Game Manager";
        public const string Instantiate = SkullDemo + "/Instantiate";
        public const string Paths = SkullDemo + "/Paths";
        public const string Persistence = SkullDemo + "/Persistence";
        public const string Projectiles = SkullDemo + "/Projectiles";
        public const string Battle = SkullDemo + "/Battle";
        public const string Dialogue = SkullDemo + "/Dialogue";
        public const string UI = SkullDemo + "/UI";

        public const string AI = SkullDemo + "AI";
        public const string CharacterStates = SkullDemo + "Character States";
    }
}