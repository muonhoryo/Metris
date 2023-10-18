using System;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IBlockDestroyingCharacter;
using static GameJam_Temple.Characters.COP.IPickUpingCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class BlockDestroingModule : Module, IBlockDestroyingModule
    {
        public event Action SelectEvent = delegate { };
        public event Action HideEvent=delegate { };
        public event Action InteractEvent=delegate { };

        [SerializeField]
        private ColorModule colorModule;
        [SerializeField]
        private GameObject Owner;

        private IDestroyableBlock DestroyableBlock=null;
        private bool CanDestroy=true;

        public bool CanDestroy_
        {
            get => CanDestroy && DestroyableBlock != null && DestroyableBlock.CanDestroy(colorModule.color, 1);
            set => CanDestroy = value;
        }
        ColorModule IBlockDestroyingModule.ColorModule_ => colorModule;
        IInteractableObject IInteractionModule.InteractableObject_=> DestroyableBlock;

        void IInteractionModule.Interact()
        {
            if (CanDestroy_)
            {
                InteractEvent();
                float xDiff = DestroyableBlock.Position_.x - Owner.transform.position.x;
                float yDiff=DestroyableBlock.Position_.y - Owner.transform.position.y;
                if (Math.Abs(xDiff) > Math.Abs(yDiff))
                {
                    Owner.transform.position = new Vector2
                        (DestroyableBlock.Position_.x + (GlobalConstants.Config.CubeSize * -Mathf.Sign(xDiff)),
                        DestroyableBlock.Position_.y);
                }
                else
                {
                    Owner.transform.position = new Vector2
                        (DestroyableBlock.Position_.x ,
                        DestroyableBlock.Position_.y + (GlobalConstants.Config.CubeSize * -Mathf.Sign(yDiff)));
                }
                DestroyableBlock.Interact();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            var block= collision.gameObject.GetComponent<IDestroyableBlock>();
            if(block != null)
            {
                if (block.CanDestroy(colorModule.color, 1) && !block.IsSelected_)
                {
                    DestroyableBlock = block;
                    DestroyableBlock.Select();
                    SelectEvent();
                }
                else
                {
                    if (!block.CanDestroy(colorModule.color, 1) && block.IsSelected_)
                    {
                        DestroyableBlock.Hide();
                        DestroyableBlock = null;
                        HideEvent();
                    }
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            var block = collision.gameObject.GetComponent<IDestroyableBlock>();
            if (block != null&&block==DestroyableBlock)
            {
                DestroyableBlock.Hide();
                DestroyableBlock = null;
                HideEvent();
            }
        }
    }
}
