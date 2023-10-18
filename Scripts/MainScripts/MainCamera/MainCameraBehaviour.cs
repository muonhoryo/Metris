using MuonhoryoLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace GameJam_Temple
{
    public sealed class MainCameraBehaviour : MonoBehaviour
    {
        public static MainCameraBehaviour Instance_ { get; private set; }
        [SerializeField]
        private Transform MainCharacter;
        [SerializeField]
        private Camera CameraComp;
        private Rect MoveLimit;
        private Rect CurrentMoveTrigger=new Rect(0,0,0,0);

        public void SetMoveLimit(Rect MoveLimit)
        {
            this.MoveLimit = MoveLimit;
            UpdateCurrentMoveTrigger(MainCharacter.position);
        }
        public void SetMoveTriggerSize(Vector2 size)
        {
            CurrentMoveTrigger = new Rect(CurrentMoveTrigger.min, size);
            UpdateCurrentMoveTrigger(MainCharacter.position);
        }

        private void UpdateCameraPosition(Vector2 newPos)
        {
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
        private void UpdateCurrentMoveTrigger(Vector2 charPos)
        {
            float x = CurrentMoveTrigger.xMin;
            float y = CurrentMoveTrigger.yMin;

            if (charPos.x > MoveLimit.xMax)
            {
                x = (MoveLimit.xMax - CurrentMoveTrigger.width);
            }
            else if (charPos.x > CurrentMoveTrigger.xMax)
            {
                x += (charPos.x - CurrentMoveTrigger.xMax);
            }
            else if (charPos.x < MoveLimit.xMin)
            {
                x = MoveLimit.xMin;
            }
            else if (charPos.x < CurrentMoveTrigger.xMin)
            {
                x = charPos.x;
            }

            if (charPos.y > MoveLimit.yMax)
            {
                y = (MoveLimit.yMax - CurrentMoveTrigger.height);
            }
            else if (charPos.y > CurrentMoveTrigger.yMax)
            {
                y += (charPos.y - CurrentMoveTrigger.yMax);
            }
            else if (charPos.y < MoveLimit.yMin)
            {
                y = MoveLimit.yMin;
            }
            else if(charPos.y < CurrentMoveTrigger.yMin)
            {
                y = charPos.y;
            }

            CurrentMoveTrigger = new Rect(x, y, CurrentMoveTrigger.width, CurrentMoveTrigger.height);
        }
        private Vector2 GetCurrentPosition()
        {
            if (!CurrentMoveTrigger.Contains(MainCharacter.position))
            {
                UpdateCurrentMoveTrigger(MainCharacter.position);
                return CurrentMoveTrigger.center;
            }
            return CurrentMoveTrigger.center;
        }
        private void LateUpdate()
        {
            Vector2 targetPos = GetCurrentPosition(); 
            UpdateCameraPosition(targetPos);
        }
        public Vector2 GetCursorPos() => CameraComp.ScreenToWorldPoint
                (new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.y));
        //UnityAPI
        private void Awake()
        {
            if (!enabled)
                enabled = true;
            Instance_ = this;
        }
        private void OnDisable() =>
            enabled = true;
    }
}
