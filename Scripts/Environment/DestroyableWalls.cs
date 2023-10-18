using GameJam_Temple.Characters.COP;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IBlockDestroyingCharacter;

namespace GameJam_Temple.Level
{
    public sealed class DestroyableWalls : MonoBehaviour
    {
        [SerializeField]
        private Component DestroyableBlockComponent;

        private IDestroyableBlock DestroyableBlock;

        private void Awake()
        {
            DestroyableBlock = DestroyableBlockComponent as IDestroyableBlock;
            DestroyableBlock.DestroyEvent += () => Destroy(gameObject);
        }
    }
}
