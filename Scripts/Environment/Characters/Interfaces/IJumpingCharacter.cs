using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public interface IJumpingCharacter:IGroundMovingCharacter
    {
        public interface IJumpingModule
        {
            public event Action JumpEvent;
            public event Action JumpingHasBeenAcceptedEvent;
            public bool CanJump_ { get; set; }
            public void Jump();
        }
        public event Action JumpEvent_
        {
            add { JumpingModule_.JumpEvent += value; }
            remove { JumpingModule_.JumpEvent -= value;}
        }
        public event Action JumpingHasBeenAcceptedEvent_
        {
            add { JumpingModule_.JumpingHasBeenAcceptedEvent += value; }
            remove { JumpingModule_.JumpingHasBeenAcceptedEvent -= value; }
        }
        public bool CanJump_ { get => JumpingModule_.CanJump_; set => JumpingModule_.CanJump_ = value; }
        public void Jump()
        {
            if (CanJump_)
                JumpingModule_.Jump();
        }

        protected IJumpingModule JumpingModule_ { get; }
    }
}
