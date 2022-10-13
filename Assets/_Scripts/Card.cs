using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SB
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _cardBackSpriteRenderer;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private Canvas _cardCanvas;
        [SerializeField] private GameObject _cardBaseImage;
        [SerializeField] private GameObject _cardHighlightImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _manaCostText;
        [SerializeField] private Image _cardImage;
        [SerializeField] private TextMeshProUGUI _growthCostText;
        [SerializeField] private TextMeshProUGUI _rulesText;
        [SerializeField] public string _cardName;
        [SerializeField] public int _cardManaCost;
        [SerializeField] public Sprite _cardSprite;
        [SerializeField] public int _cardGrowthCost;
        [SerializeField] public string _cardRulesText;
        [SerializeField] public int _foodYeild;
        [SerializeField] public int _flowerYeild;
        [SerializeField] public int _manaYeild;

        
        [SerializeField] private Color _dryColor;
        [SerializeField] private Color _wateredColor;
        private bool _isSelected;
        private bool _isEnlarged;
        private bool _isWatered;
        private float _cardZValue = -0.001f;
        public CardType _cardType;
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
            if((HandManager.Instance._cardSelected!=null)&&(HandManager.Instance._cardSelected._cardType == CardType.Spell))
            {
                if(_cardTile != null)
                {
                    HandManager.Instance.PlaceCardSelected(_cardTile);
                }
            }
            if(!_isSelected)
                HandManager.Instance.SelectSingleCard(this);
        }
        private void OnMouseEnter()
        {
            HighlightCard();
            EnlargeCard();
        }
        private void OnMouseExit()
        {
            if(!_isSelected)
                UnhighlightCard();
            UnenlargeCard();
        }
        public void SelectCard()
        {
            _isSelected = true;
            HighlightCard();
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
            
            _isWatered = false;
            _cardBackSpriteRenderer.color = _dryColor;
            _cardBaseImage.GetComponent<Image>().color = _dryColor;
        }
    }
}

public enum CardType 
{
  Garden,
  Spell
}
