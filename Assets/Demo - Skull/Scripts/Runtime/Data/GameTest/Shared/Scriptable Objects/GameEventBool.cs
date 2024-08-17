using UnityEngine;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull
{
    [CreateAssetMenu(menuName = MenuPath.Data + "Events/Game Event Bool", fileName = "NewGameEventBool")]
    public class GameEventBool : GameEvent<bool> { }
}