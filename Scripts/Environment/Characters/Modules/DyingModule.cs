using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IDyingCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class DyingModule : Module,IDyingModule
    {
        public event Action DeathEvent = delegate { };

        [SerializeField]
        private Transform Owner;

        private ICheckPoint CheckPoint = null;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out ICheckPoint checkPoint)&&
                checkPoint!=CheckPoint)
            {
                CheckPoint= checkPoint;
            }
        }
        void IDyingModule.Death()
        {
            if (CheckPoint != null)
            {
                Owner.position = CheckPoint.CheckPointPosition_;
                DeathEvent();
            }
        }
    }
}
