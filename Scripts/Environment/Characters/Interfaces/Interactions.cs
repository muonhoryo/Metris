using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam_Temple.Characters.COP
{
    public interface IInteractableObject
    {
        public bool IsSelected_ { get; }
        public void Select();
        public void Hide();
        public void Interact();
    }
    public interface IInteractionModule
    {
        public event Action SelectEvent;
        public event Action HideEvent;
        public event Action InteractEvent;
        public void Interact();

        protected IInteractableObject InteractableObject_ { get; }
    }
}
