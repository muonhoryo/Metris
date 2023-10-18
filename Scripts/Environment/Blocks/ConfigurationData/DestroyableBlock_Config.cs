using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    [Serializable]
    [CreateAssetMenu]
    public sealed class DestroyableBlock_Config : ScriptableObject
    {
        public int DestroyableBlockLayerMask;
        [Range(0,1)]
        public float SelectedAlphaValue;
        [Range(0,1)]
        public float HidingAlphaValue;
    }
}
