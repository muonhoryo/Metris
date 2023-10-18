using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJam_Temple.Characters.COP
{
    public interface IDyingCharacter
    {
        public interface IDyingModule
        {
            public event Action DeathEvent;

            public void Death();
        }
        public interface ICheckPoint
        {
            public Vector2 CheckPointPosition_ { get; }
        }
        public event Action DeathEvent_
        {
            add { DyingModule_.DeathEvent += value; }
            remove { DyingModule_.DeathEvent -= value;}
        }

        public void Death()
        {
            DyingModule_.Death();
        }

        protected IDyingModule DyingModule_ { get; }
    }
}
