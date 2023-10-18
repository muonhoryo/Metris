using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameJam_Temple.Characters.COP
{
    public abstract class Module : MonoBehaviour,IModule
    {
        event Action IModule.ActivateEvent
        {
            add { ActivateEvent += value; }
            remove { ActivateEvent -= value; }
        }
        event Action IModule.DeactivateEvent
        {
            add { DeactivateEvent += value; }
            remove { DeactivateEvent -= value; }
        }
        protected event Action ActivateEvent=delegate { };
        protected event Action DeactivateEvent=delegate { };
        private bool IsActive = false;
        protected bool IsActive_
        {
            get => IsActive;
            set
            {
                bool oldValue = IsActive;
                IsActive = value;
                if (oldValue != value)
                {
                    if (value)
                        ActivateEvent();
                    else
                        DeactivateEvent();
                }
            }
        }
        protected virtual bool CanTurnActivityFromOutside_ => true;
        bool IModule.IsActive_ { get => IsActive_; set { if (CanTurnActivityFromOutside_) IsActive_ = value; } }
    }
}
