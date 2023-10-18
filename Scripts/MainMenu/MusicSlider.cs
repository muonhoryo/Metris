using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.MainMenu
{
    public sealed class MusicSlider : SoundsSllider
    {
        protected override float Variable_ { set => Registry.MusicLevel = value; }
    }
}
