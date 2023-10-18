using GameJam_Temple.Characters;
using GameJam_Temple.Characters.COP;
using GameJam_Temple.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple
{
    public sealed class TutorialMessages : MonoBehaviour
    {
        public string MovingTutorialMessage = "Press moving buttons.";
        public string JumpingTutorialMessage = "Press button to jump.";
        public string DestroyFigureTutorialMessage = "Press button to destroy selected blocks. " +
            "Blocks select, when you and blocks form figure at once color.";
        public string CollectColorTutorialMessage = "Press button to collect new colors.";
        public string ChangeColorTutorialMessage = "Press button to change your current color.";

        public string MovingTutorialKeyName = "A D";
        public string JumpingTutorialKeyName = "Spc";
        public string DestroyFigureTutorialKeyName = "E";
        public string CollectColorTutorialKeyName = "F";
        public string ChangeColorTutorialKeyName = "Q";

        public float MovingTutorialDelay = 5;
        public float JumpingTutorialDelay = 5;
        public float DestroyFigureDelay = 5;
        public float CollectColorDelay = 5;
        public float ChangeColorDelay = 5;

        public float AxisUpperOffset;

        [SerializeField]
        private GameObject AxisMessagePrefab;
        [SerializeField]
        private GameObject MessagePrefab;

        private GameObject PrevMessage=null;
        private void CreateAxisMessage(string mainMess,string axis,Vector2 position,float delay)
        {
            if(PrevMessage!=null)
                Destroy(PrevMessage);
            PrevMessage = Registry.Instantiate(AxisMessagePrefab,
                position,Quaternion.Euler(0,0,0),Registry.GUItransform);
            var message = PrevMessage.GetComponent<AxisMessage>();
            message.MessageText.text = mainMess;
            message.ButtonText.text = axis;
            message.DeathDelay = delay;
        }
        void MovingTutorialAction(IGroundMovingCharacter.IFallingCheckingModule.LandingInfo i)
        {
            CreateAxisMessage(MovingTutorialMessage, MovingTutorialKeyName,
                new Vector2(Registry.Maincharacter.transform.position.x, 
                Registry.Maincharacter.transform.position.y + AxisUpperOffset),
                MovingTutorialDelay);
            (Registry.Maincharacter as IGroundMovingCharacter).LandingEvent_ -= MovingTutorialAction;
        }
        void JumpingTutorialAction(int i)
        {
            CreateAxisMessage(JumpingTutorialMessage, JumpingTutorialKeyName,
                new Vector2(Registry.Maincharacter.transform.position.x, 
                Registry.Maincharacter.transform.position.y + AxisUpperOffset),
                JumpingTutorialDelay);
            (Registry.Maincharacter as IGroundMovingCharacter).StartMovingEvent_-= JumpingTutorialAction;
        }
        void DestroyFigureAction()
        {
            CreateAxisMessage(DestroyFigureTutorialMessage, DestroyFigureTutorialKeyName,
                new Vector2(Registry.Maincharacter.transform.position.x, 
                Registry.Maincharacter.transform.position.y + AxisUpperOffset),
                DestroyFigureDelay);
            (Registry.Maincharacter as IBlockDestroyingCharacter).SelectDestroyableBlockEvent_ -= DestroyFigureAction;
        }
        void CollectColorAction()
        {
            CreateAxisMessage(CollectColorTutorialMessage, CollectColorTutorialKeyName,
                new Vector2(Registry.Maincharacter.transform.position.x,
                Registry.Maincharacter.transform.position.y + AxisUpperOffset),
                CollectColorDelay);
            (Registry.Maincharacter as IPickUpingCharacter).SelectItemEvent_ -= CollectColorAction;
        }
        void ChangeColorAction()
        {
            CreateAxisMessage(ChangeColorTutorialMessage, ChangeColorTutorialKeyName,
                new Vector2(Registry.Maincharacter.transform.position.x,
                Registry.Maincharacter.transform.position.y + AxisUpperOffset),
                ChangeColorDelay);
            (Registry.Maincharacter as IPickUpingCharacter).PickUpEvent_ -= ChangeColorAction;
        }
        private void Start()
        {
            (Registry.Maincharacter as IGroundMovingCharacter).LandingEvent_ += MovingTutorialAction;
            (Registry.Maincharacter as IGroundMovingCharacter).StartMovingEvent_ += JumpingTutorialAction;
            (Registry.Maincharacter as IBlockDestroyingCharacter).SelectDestroyableBlockEvent_ += DestroyFigureAction;
            (Registry.Maincharacter as IPickUpingCharacter).SelectItemEvent_ += CollectColorAction;
            (Registry.Maincharacter as IPickUpingCharacter).PickUpEvent_ += ChangeColorAction;
            Destroy(this);
        }
    }
}
