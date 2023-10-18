using GameJam_Temple.Characters.COP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    public sealed class ColorUnlocker : MonoBehaviour,IPickUpingCharacter.IPickableItem
    {
        [SerializeField]
        private ColorModule.Color Color;
        public bool IsSelected_ { get; private set; } = false;
        public void Select() { IsSelected_ = true; }
        public void Hide() { IsSelected_ = false; }
        public void Interact()
        {
            Registry.AcceptableColorsModule.AddColor(Color);
            Destroy(gameObject);
        }
    }
}
