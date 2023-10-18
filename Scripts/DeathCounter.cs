using GameJam_Temple.Characters.COP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam_Temple.GUI
{
    public class DeathCounter : MonoBehaviour
    {
        [SerializeField]
        private Text deathText;
        [SerializeField]
        private int StartCount = 0;
        public int deathCount { get; private set; }

        private void Start()
        {
            (Registry.Maincharacter as IDyingCharacter).DeathEvent_ += IncrementDeathCount;
            ResetDeathCount();
        }

        public void IncrementDeathCount()
        {
            deathCount++;
            UpdateDeathText();
        }

        public void ResetDeathCount()
        {
            deathCount = StartCount;
            UpdateDeathText();
        }

        private void UpdateDeathText()
        {
            deathText.text = "Death Count: " + deathCount.ToString();
        }
    }
}
