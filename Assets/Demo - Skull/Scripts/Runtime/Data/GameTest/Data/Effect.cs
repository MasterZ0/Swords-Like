using System;

namespace Z3.DemoSkull.Data
{
    [Serializable]
    public abstract class Effect
    {
        public virtual void Start() { }
        public virtual bool Update() => true;
        public virtual void End() { }
    }
}