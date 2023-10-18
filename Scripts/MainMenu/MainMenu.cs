using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJam_Temple.MainMenu
{
    public sealed class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private string GameLocationSceneName;
        [SerializeField]
        private GameObject SettingsMenu;
        public void StartGame()
        {
            SceneManager.LoadScene(GameLocationSceneName, LoadSceneMode.Single);
        }
        public void OpenSettingsMenu()
        {
            SettingsMenu.SetActive(!SettingsMenu.activeSelf);
        }
        public void Exit()
        {
            Application.Quit();
        }
    }
}
