using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Color _baseColor, _offsetColor;
        [SerializeField] private SpriteRenderer _tileSpriteRenderer;
        [SerializeField] private GameObject _highlight;
        private Card _tileCard;
        public int _tileX;
        public int _tileY;
        public bool _isHover;
        public bool _hasCard;
        private void Update()
        {
            if(_isHover)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    OnMouseExit();
                    OnMouseEnter();
                }
            }
        }
        public void Init(bool isOffset,int tileX,int tileY)
        {
            _tileSpriteRenderer.color = isOffset ? _offsetColor : _baseColor;
            _tileX=tileX;
            _tileY=tileY;
        }
        public SpriteRenderer GetSpriteRenderer()
        {
            return _tileSpriteRenderer;
        }
        private void OnMouseEnter()
        {
            _isHover=true;
            TileSelectionManager.Instance.SelectMultipleTiles(_tileX,_tileY);
        }
        private void OnMouseExit()
        {
            _isHover=false;
            TileSelectionManager.Instance.UnselectTiles();
        }
        public void HightlightTile()
        {
            _highlight.SetActive(true);
        }
        public void UnhightlightTile()
        {
            _highlight.SetActive(false);
        }
        private void OnMouseDown()
        {
            if(_hasCard)
                return;
            //Movement
            //Move the chosen card to an empty tile
            if((GardenMovementManager.Instance._isMoving)&&(_tileCard==null))
            {
                GardenMovementManager.Instance.PlaceGardenCardToMove(this);
            }
            //

            if(HandManager.Instance._cardSelected==null)
                return;
            
            HandManager.Instance.PlaceCardSelectedInSelectedTiles();
        }
        public void SetTileCard(Card card)
        {
            _tileCard = card;
            _hasCard = true;
        }
        public Card PeekTileCard()
        {
            return _tileCard;
        }
        public void RemoveTileCard()
        {
            _tileCard=null;
            _hasCard = false;
        }
        public void HarvestCardOnTile()
        {
            if(_tileCard!=null)
                _tileCard.HarvestCard();
        }
        public void StartTurnCardOnTile()
        {
            if(_tileCard!=null)
                _tileCard.OnTurnStart();
        }
        public void WaterCardOnTile()
        {
            if(_tileCard!=null)
                _tileCard.OnWater();
        }
    }
}
