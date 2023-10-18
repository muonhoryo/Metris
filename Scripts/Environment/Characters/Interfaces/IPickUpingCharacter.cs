using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public interface IPickUpingCharacter
    {
        public interface IPickableItem : IInteractableObject { }
        public interface IPickUpingModule : IInteractionModule { }
        public event Action SelectItemEvent_
        {
            add { PickUpingModule_.SelectEvent += value; }
            remove { PickUpingModule_.SelectEvent -= value; }
        }
        public event Action HideItemEvent_
        {
            add { PickUpingModule_.HideEvent += value; }
            remove { PickUpingModule_.HideEvent -= value; }
        }
        public event Action PickUpEvent_
        {
            add { PickUpingModule_.InteractEvent += value; }
            remove { PickUpingModule_.InteractEvent -= value; }
        }
        public void PickUp()
        {
            PickUpingModule_.Interact();
        }

        protected IPickUpingModule PickUpingModule_ { get; }
    }
}
