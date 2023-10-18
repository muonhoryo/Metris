using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public interface IBlockDestroyingCharacter
    {
        public interface IDestroyableBlock:IInteractableObject
        {
            public event Action DestroyEvent;
            protected ColorModule.Color BlockColor_ { get; }
            public Vector2 Position_ { get; }
            protected int VertexesCount_ { get; }
            public bool CanDestroy(ColorModule.Color destroyingColor, int destroyingCount) 
            {
                return destroyingColor == BlockColor_ && destroyingCount + VertexesCount_ >= 4;
            }
        }
        public interface IBlockDestroyingModule:IInteractionModule
        {
            public bool CanDestroy_ { get; set; }

            protected ColorModule ColorModule_ { get; }
        }

        public event Action DestroyBlockEvent_
        {
            add { BlockDestroyingModule_.InteractEvent += value; }
            remove { BlockDestroyingModule_.InteractEvent -= value; }
        }
        public event Action SelectDestroyableBlockEvent_
        {
            add { BlockDestroyingModule_.SelectEvent += value; }
            remove { BlockDestroyingModule_.SelectEvent -= value;}
        }

        public bool CanDestroy_ 
        { get => BlockDestroyingModule_.CanDestroy_;
            set => BlockDestroyingModule_.CanDestroy_ = value;
        }

        public void Destroy()
        {
            BlockDestroyingModule_.Interact();
        }

        protected IBlockDestroyingModule BlockDestroyingModule_ { get; }
    }
}
