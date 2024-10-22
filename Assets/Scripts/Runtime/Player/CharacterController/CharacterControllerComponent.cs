﻿using Z3.GMTK2024.Data;

namespace Z3.GMTK2024
{
    public abstract class CharacterControllerComponent
    { 
        protected CharacterPawn Controller { get; private set; }
        protected CharacterData Data => Controller.Data;

        public virtual void Init(CharacterPawn controller)
        {
            Controller = controller;
        }
    }

}