using System;

namespace GameJam_Temple.Characters.COP
{
    public interface IModule
    {
        public event Action ActivateEvent;
        public event Action DeactivateEvent;
        public bool IsActive_ { get; set; }
    }
}
