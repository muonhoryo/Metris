using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.MainMenu
{
    public class SoundsSllider : MonoBehaviour
    {
        protected virtual float Variable_ { set => Registry.SoundsLevel = value; }
        public void ChangeValue(float value)
        {
            Variable_= value;
        }
    }
}
