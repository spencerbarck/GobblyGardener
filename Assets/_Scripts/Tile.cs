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
        private GridManager _gridManager;
        public bool _hasCard;
        public float _tileValue;
        private Card _tileCard;
        private void Start()
        {
            _gridManager = FindObjectOfType<GridManager>();
            _tileValue = 222f;
        }
        public void Init(bool isOffset)
        {
            _renderer.color = isOffset ? _offsetColor : _baseColor;
        }

        public SpriteRenderer GetSpriteRenderer()
        {
            return _renderer;
        }
        private void OnMouseEnter()
        {
            HightlightTile();
            _gridManager.FocusTile(this);
        }
        private void OnMouseExit()
        {
            UnhightlightTile();
            _gridManager.UnFocusTile();
        }
        private void HightlightTile()
        {
            _highlight.SetActive(true);
        }
        private void UnhightlightTile()
        {
            _highlight.SetActive(false);
        }
        private void OnMouseDown()
        {
            if((HandManager.Instance._cardSelected!=null))
            {
                if(HandManager.Instance._cardSelected._cardType == CardType.Spell)
                {
                    HandManager.Instance.PlaceCardSelected(this);
                } 
                else if(HandManager.Instance._cardSelected._cardType == CardType.Garden)
                {
                    if(!_hasCard)
                        HandManager.Instance.PlaceCardSelected(this);
                }
            }
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
