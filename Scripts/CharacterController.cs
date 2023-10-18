using GameJam_Temple.Characters;
using GameJam_Temple.Characters.COP;
using GameJam_Temple.Exceptions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJam_Temple.Control
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private Component ControlledCharacterComponent;
        [SerializeField]
        private string MainMenuSceneName;

        private IHumanCharacter ControlledCharacter;
        private const string InputName_Horizontal="Horizontal";
        private const string InputName_Jump = "Jump";
        private const string InputName_DestroyBlock = "DestroyBlock";
        private const string InputName_ChangeColor = "ChangeColor";
        private const string InputName_PickUp = "PickUp";
        private const string InputName_MainMenu = "MainMenu";
        private void Update()
        {
            float axis = Input.GetAxisRaw(InputName_Horizontal);
            if (axis != 0)
            {
                int direction = axis > 0 ? 1 : -1;
                ControlledCharacter.SetDirection(direction);
                ControlledCharacter.StartMoving();
            }
            else
            {
                ControlledCharacter.StopMoving();
            }
            if(Input.GetButtonDown(InputName_Jump))
            {
                ControlledCharacter.Jump();
            }
            if (Input.GetButtonDown(InputName_DestroyBlock))
            {
                ControlledCharacter.Destroy();
            }
            else if (Input.GetButtonDown(InputName_ChangeColor))
            {
                ControlledCharacter.ChangeColor();
            }
            if (Input.GetButtonDown(InputName_PickUp))
            {
                ControlledCharacter.PickUp();
            }
            if (Input.GetButtonDown(InputName_MainMenu))
            {
                SceneManager.LoadScene(MainMenuSceneName, LoadSceneMode.Single);
            }
        }
        private void Awake()
        {
            if (ControlledCharacterComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("ControlledCharacterComponent");

            ControlledCharacter=ControlledCharacterComponent as IHumanCharacter;
            if (ControlledCharacter == null)
                throw GameJam_Exception.GetWrondModuleType<IHumanCharacter>("ControlledCharacter");
        }
    }
}
