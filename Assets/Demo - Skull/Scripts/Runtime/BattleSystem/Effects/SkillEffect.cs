using Z3.UIBuilder.Core;

namespace Z3.DemoSkull.BattleSystem
{
    [System.Serializable, HideReferenceObjectPicker, InlineProperty]
    public abstract class SkillEffect 
    {
        public abstract void Start();
        public abstract bool Update();
        public abstract void Dispose();
    }
}
