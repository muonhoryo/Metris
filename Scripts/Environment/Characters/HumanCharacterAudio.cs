using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public sealed class HumanCharacterAudio : Module
    {
        [SerializeField]
        private Component HumanCharacterComponent;
        [SerializeField]
        private Component ColorCollectorComponent;

        [SerializeField]
        private AudioSource Source;
        [SerializeField]
        private AudioClip[] SelectAudio;
        [SerializeField]
        private AudioClip[] DeathAudio;
        [SerializeField]
        private AudioClip[] JumpAudio;
        [SerializeField]
        private AudioClip[] LandingAudio;
        [SerializeField]
        private AudioClip[] CollectAudio;

        private void RunAudio(AudioClip[] clip)
        {
            Source.PlayOneShot(clip[Random.Range(0, clip.Length)]);
        }
        private void Start()
        {
            Source.volume = Registry.SoundsLevel;
            var chr = HumanCharacterComponent as IHumanCharacter;
            var collector = ColorCollectorComponent as IColorChangableCharacter.IAcceptableColorsModule;
            chr.SelectDestroyableBlockEvent_ += () => RunAudio(SelectAudio);
            chr.DeathEvent_ += () => RunAudio(DeathAudio);
            chr.JumpEvent_+=() => RunAudio(JumpAudio);
            chr.LandingEvent_ += (i) => RunAudio(LandingAudio);
            collector.CollectColorEvent += () => RunAudio(CollectAudio);
        }

        protected override bool CanTurnActivityFromOutside_ => false;
    }
}

