using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJam_Temple.Level
{
    public sealed class EndGameAnimation : MonoBehaviour
    {
        [SerializeField]
        private string MainMenuSceneName;
        public void EndGame()
        {
            SceneManager.LoadScene(MainMenuSceneName,LoadSceneMode.Single);
        }
    }
}
