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
        private void Start()
        {
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
            TileSelectionManager.Instance.SelectTile(GridManager.Instance._tileDictionary[new Vector2(_tileX,_tileY+1)]);
            TileSelectionManager.Instance.SelectTile(GridManager.Instance._tileDictionary[new Vector2(_tileX,_tileY-1)]);
            TileSelectionManager.Instance.SelectTile(this);
        }
        private void OnMouseExit()
        {
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
            foreach(Tile tile in TileSelectionManager.Instance._tilesSlected)
            {
                if((HandManager.Instance._cardSelected!=null))
                {
                    if(HandManager.Instance._cardSelected._cardType == CardType.Spell)
                    {
                        HandManager.Instance.PlaceCardSelected(tile);
                    } 
                    else if(HandManager.Instance._cardSelected._cardType == CardType.Garden)
                    {
                        if(!_hasCard)
                            HandManager.Instance.PlaceCardSelected(tile);
                    }
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
