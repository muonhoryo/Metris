using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using MuonhoryoLibrary.PathFinding2D;
using static GameJam_Temple.Characters.COP.IBlockDestroyingCharacter;
using GameJam_Temple.Characters.COP;
using System;

namespace GameJam_Temple.Level
{
    public sealed class DestroyableBlock :Module, IDestroyableBlock
    {
        public event Action DestroyEvent = delegate { };
        [SerializeField]
        private DestroyableBlock_Config Config;
        [SerializeField]
        private ColorModule.Color color;
        [SerializeField]
        private Rigidbody2D NeighboursChecker;
        [SerializeField]
        private SpriteRenderer BaseSprite;

        private bool IsSelected=false;

        ColorModule.Color IDestroyableBlock.BlockColor_ => color;
        int IDestroyableBlock.VertexesCount_ =>Neighbours_!=null?GetVertexCountOfGraph():1;
        bool IInteractableObject.IsSelected_ => IsSelected;
        Vector2 IDestroyableBlock.Position_ => transform.position;

        public void Interact()
        {
            if (!IsInitializing)
            {
                IsInitializing = true;
                foreach (var nei in Neighbours_)
                {
                    nei.Interact();
                }
                Destroy(gameObject);
                DestroyEvent();
            }
        }
        public void Hide()
        {
            if (IsSelected)
            {
                IsSelected = false;
                BaseSprite.color = new Color
                    (BaseSprite.color.r,
                    BaseSprite.color.g,
                    BaseSprite.color.b,
                    Config.HidingAlphaValue);
                foreach (var nei in Neighbours_)
                    nei.Hide();
            }
        }
        public void Select()
        {
            if (!IsSelected)
            {
                IsSelected = true;
                BaseSprite.color =new Color
                    (BaseSprite.color.r,
                    BaseSprite.color.g,
                    BaseSprite.color.b,
                    Config.SelectedAlphaValue);
                foreach (var nei in Neighbours_)
                    nei.Select();
            }
        }
        private List<DestroyableBlock> Neighbours_ { get; set; } = null;
        private bool IsInitializing = false;
        public void Initialize()
        {
            if (!IsInitializing)
            {
                IsInitializing = true;
                if (Neighbours_ != null)
                {
                    foreach (var nei in Neighbours_)
                    {
                        nei.Initialize();
                    }
                }
                Neighbours_ = new List<DestroyableBlock> { };
                List<Collider2D> colliders = new List<Collider2D>();
                ContactFilter2D filter = new ContactFilter2D();
                filter.useTriggers = false;
                filter.layerMask = Config.DestroyableBlockLayerMask;
                NeighboursChecker.OverlapCollider(filter, colliders);
                colliders = colliders.Where((coll) =>
                    coll.gameObject.TryGetComponent<DestroyableBlock>(out var i) && i != this).ToList();
                foreach (var coll in colliders)
                {
                    DestroyableBlock block = coll.gameObject.GetComponent<DestroyableBlock>();
                    if(block.color==color)
                        Neighbours_.Add(block);
                }
                IsInitializing = false;
            }
        }
        public int GetVertexCountOfGraph()
        {
            List<DestroyableBlock> vertexList = new List<DestroyableBlock> {this };
            void CountVertex(List<DestroyableBlock> vertexNeighbours)
            {
                foreach(var nei in vertexNeighbours)
                {
                    if (!vertexList.Contains(nei))
                    {
                        vertexList.Add(nei);
                        if (nei.Neighbours_ != null)
                            CountVertex(nei.Neighbours_);
                    }
                }
            }
            CountVertex(Neighbours_);
            return vertexList.Count;
        }

        private void Awake()
        {
            Initialize();
        }
        private void Start()
        {
            BaseSprite.color = color.GetUnityColor();
        }
    }
}
