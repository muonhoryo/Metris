using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam_Temple.GUI
{
    public class Message : MonoBehaviour
    {
        public Text MessageText;
        public float DeathDelay;
        private void Start()
        {
            StartCoroutine(Delay());
        }
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(DeathDelay);
            Destroy(gameObject);
        }
    }
}
