using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SB
{
    public class Card : MonoBehaviour
    {
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
        private bool _isSelected;
        private bool _isEnlarged;
        private float _cardZValue = -0.001f;
        private void Start()
        {
            _cardBaseImage.SetActive(false);
            _cardHighlightImage.SetActive(false);
            _nameText.text = _cardName;
            _manaCostText.text = _cardManaCost.ToString();
            _cardImage.sprite = _cardSprite;
            _growthCostText.text = _cardGrowthCost.ToString();
            _rulesText.text = _cardRulesText;
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
    }
}
