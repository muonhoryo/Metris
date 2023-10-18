using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple
{
    [Serializable]
    [CreateAssetMenu]
    public sealed class GlobalConstants_Config : ScriptableObject
    {
        public float CubeSize;
        public Color CyanColor;
        public Color BlueColor;
        public Color OrangeColor;
        public Color YellowColor;
        public Color GreenColor;
        public Color PurpleColor;
        public Color RedColor;
        public int GroundLayerMask;
        public int CharacterLayerMask;
        public int BulletCollisionLayerMask;
    }
}
