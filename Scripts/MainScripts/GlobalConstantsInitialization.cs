using GameJam_Temple.Characters.COP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple
{
    public sealed class GlobalConstantsInitialization : MonoBehaviour
    {
        [SerializeField]
        private GlobalConstants_Config Config;
        private void Awake()
        {
            GlobalConstants.Config = Config;
            Destroy(this);
        }
    }
}
