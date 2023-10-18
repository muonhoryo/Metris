
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IPickUpingCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class PickUpingModule : MonoBehaviour, IPickUpingModule
    {
        public event Action SelectEvent = delegate { };
        public event Action HideEvent = delegate { };
        public event Action InteractEvent = delegate { };

        private IPickableItem InteractableObject=null;
        IInteractableObject IInteractionModule.InteractableObject_ => InteractableObject;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out IPickableItem obj)&&
                InteractableObject!=obj)
            {
                InteractableObject = obj;
                InteractableObject.Select();
                SelectEvent();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IPickableItem obj) &&
                obj == InteractableObject)
            {
                InteractableObject.Hide();
                InteractableObject = null;
                HideEvent();
            }
        }

        void IInteractionModule.Interact()
        {
            if (InteractableObject != null)
            {
                InteractableObject.Interact();
                InteractEvent();
            }
        }
    }
}
