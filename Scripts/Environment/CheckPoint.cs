using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IDyingCharacter;

namespace GameJam_Temple.Level
{
    public sealed class CheckPoint : MonoBehaviour,ICheckPoint
    {
        [SerializeField]
        private Vector2 SpawnPosition;
        Vector2 ICheckPoint.CheckPointPosition_ => SpawnPosition;
    }
}
