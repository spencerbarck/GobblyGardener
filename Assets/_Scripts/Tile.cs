using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Color _baseColor, _offsetColor;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private GameObject _highlight;
        public bool _hasCard;
        private Card _tileCard;
        public int _tileX;
        public int _tileY;
        public bool _isHover;
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
            _renderer.color = isOffset ? _offsetColor : _baseColor;
            _tileX=tileX;
            _tileY=tileY;
        }
        public SpriteRenderer GetSpriteRenderer()
        {
            return _renderer;
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
            if(HandManager.Instance._cardSelected==null)
                return;
            
            HandManager.Instance.PlaceCardSelectedInSelectedTiles();
        }
        public void SetTileCard(Card card)
        {
            _tileCard = card;
            _hasCard = true;
        }
        public void HarvestCardOnTile()
        {
            if(_tileCard!=null)
                _tileCard.HarvestCard();
        }
        public void WaterCardOnTile()
        {
            if(_tileCard!=null)
                _tileCard.WaterCard();
        }
    }
}
