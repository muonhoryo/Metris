using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public sealed class HumanCharacterMusic : MonoBehaviour
    {
        [SerializeField]
        private AudioSource Source;
        private void Start()
        {
            Source.volume = Registry.MusicLevel;
        }
    }
}
