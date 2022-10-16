using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SB
{
    public class Card : MonoBehaviour
    {
        public CardType _cardType;
        public TileSelectionType _cardTileSelectionType;
        [Header("UI Settings")]
        [SerializeField] private Canvas _cardCanvas;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _manaCostText;
        [SerializeField] private Image _cardImage;
        [SerializeField] private TextMeshProUGUI _growthCostText;
        [SerializeField] private TextMeshProUGUI _rulesText;
        [SerializeField] public string _cardRulesText;
        [Header("Resource Settings")]
        [SerializeField] public int _cardManaCost;
        [SerializeField] public int _cardGrowthCost;
        [SerializeField] public int _foodYeild;
        [SerializeField] public int _flowerYeild;
        [SerializeField] public int _manaYeild;

        [Header("Appearance Settings")]
        [SerializeField] private Color _dryColor;
        [SerializeField] private Color _wateredColor;
        [SerializeField] public string _cardName;
        [SerializeField] public Sprite _cardSprite;
        [SerializeField] private GameObject _cardBaseImage;
        [SerializeField] private GameObject _cardHighlightImage;
        [SerializeField] private SpriteRenderer _cardBackSpriteRenderer;
        [SerializeField] private GameObject _highlight;
        private bool _isSelected;
        private bool _isEnlarged;
        private bool _isWatered;
        private float _cardZValue = -0.001f;
        [HideInInspector] public Tile _cardTile;
        private void Start()
        {
            _cardBaseImage.SetActive(false);
            _cardHighlightImage.SetActive(false);
            _nameText.text = _cardName;
            _manaCostText.text = _cardManaCost.ToString();
            _cardImage.sprite = _cardSprite;
            _growthCostText.text = _cardGrowthCost.ToString();
            _rulesText.text = _cardRulesText;

            _cardBackSpriteRenderer.color = _dryColor;
            _cardBaseImage.GetComponent<Image>().color = _dryColor;
        }
        private void Update()
        {
            if(_isEnlarged)
            {
                transform.position = new Vector3(transform.position.x,transform.position.y,_cardZValue-0.01f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x,transform.position.y,_cardZValue);
            }
        }
        private void OnMouseDown()
        {
            //Used if card is in the garden
            if((HandManager.Instance._cardSelected!=null)&&(HandManager.Instance._cardSelected._cardType == CardType.Spell))
            {
                if(_cardTile != null)
                {
                    HandManager.Instance.PlaceCardSelectedInSelectedTiles();
                }
            }
            //Used if card is in the hand
            if(!_isSelected)
                HandManager.Instance.SelectSingleCard(this);
        }
        private void OnMouseEnter()
        {
            HighlightCard();
            EnlargeCard();
            if(_cardTile!=null)
            {
                _cardTile._isHover=true;
                TileSelectionManager.Instance.SelectMultipleTiles(_cardTile._tileX,_cardTile._tileY);
            }
        }
        private void OnMouseExit()
        {
            if(!_isSelected)
                UnhighlightCard();
            UnenlargeCard();
            if(_cardTile!=null)
            {
                _cardTile._isHover=false;
                TileSelectionManager.Instance.UnselectTiles();
            }
        }
        public Card SelectCard()
        {
            TileSelectionManager.Instance._tileSelectionType = _cardTileSelectionType;
            _isSelected = true;
            HighlightCard();
            return this;
        }
        public void UnSelectCard()
        {
            _isSelected = false;
            UnhighlightCard();
        }
        public void HighlightCard()
        {
            _highlight.SetActive(true);
        }
        public void UnhighlightCard()
        {
            _highlight.SetActive(false);
        }
        public void EnlargeCard()
        {
            _isEnlarged = true;
            transform.localScale = transform.localScale*2;
            _cardCanvas.sortingOrder = 17;
            
            _cardBaseImage.SetActive(true);
            _cardHighlightImage.SetActive(true);
        }
        public void UnenlargeCard()
        {
            _isEnlarged = false;
            transform.localScale = transform.localScale/2;
            _cardCanvas.sortingOrder = 16;

            _cardBaseImage.SetActive(false);
            _cardHighlightImage.SetActive(false);
        }
        public void WaterCard()
        {
            _isWatered = true;
            _cardBackSpriteRenderer.color = _wateredColor;
            _cardBaseImage.GetComponent<Image>().color = _wateredColor;
        }
        private void DryCard()
        {
            _isWatered = false;
            _cardBackSpriteRenderer.color = _dryColor;
            _cardBaseImage.GetComponent<Image>().color = _dryColor;
        }
        public void HarvestCard()
        {
            if(_isWatered)
            {
                if(_cardGrowthCost>0)
                {
                    _cardGrowthCost--;
                    _growthCostText.text = _cardGrowthCost.ToString();
                }
                else
                {
                    ResourcesManager.Instance._flowerCount += _flowerYeild;
                    ResourcesManager.Instance._foodCount += _foodYeild;
                    ResourcesManager.Instance._manaCount += _manaYeild;
                }
            }
            DryCard();
        }
    }
}

public enum CardType 
{
  Garden,
  Spell
}
