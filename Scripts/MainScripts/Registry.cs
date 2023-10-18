using GameJam_Temple.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IColorChangableCharacter;

namespace GameJam_Temple
{
    public sealed class Registry : MonoBehaviour
    {
        public static IAcceptableColorsModule AcceptableColorsModule { get; private set; }
        public static Transform GUItransform { get; private set; }
        public static Transform Scenetransform { get; private set; }
        public static HumanCharacter Maincharacter { get; private set; }
        public static float SoundsLevel=1;
        public static float MusicLevel=1;
        [SerializeField]
        private Component AcceptableColorsModuleComponent;
        [SerializeField]
        private Transform GUITransform;
        [SerializeField]
        private Transform SceneTransform;
        [SerializeField]
        private HumanCharacter MainCharacter;
        private void Awake()
        {
            AcceptableColorsModule = AcceptableColorsModuleComponent as IAcceptableColorsModule;
            GUItransform = GUITransform;
            Scenetransform = SceneTransform;
            Maincharacter = MainCharacter;
        }
    }
}
