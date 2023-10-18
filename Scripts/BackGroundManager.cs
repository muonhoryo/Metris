using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameJam_Temple.GUI
{
    public sealed class BackGroundManager : MonoBehaviour
    {
        [SerializeField]
        private Transform MainCamera;
        [SerializeField]
        private float backgroundScale = 0.5f;
        private void LateUpdate()
        {
            transform.position = MainCamera.transform.position * backgroundScale;
        }
    }
}
