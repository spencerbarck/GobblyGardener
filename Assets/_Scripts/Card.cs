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
        private float _cardZValue = -0.001f;
        private void Start()
        {
            _nameText.text = _cardName;
            _manaCostText.text = _cardManaCost.ToString();
            _cardImage.sprite = _cardSprite;
            _growthCostText.text = _cardGrowthCost.ToString();
            _rulesText.text = _cardRulesText;
        }
        private void Update()
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,_cardZValue);
        }
        private void OnMouseDown()
        {
            if(!_isSelected)
                HandManager.Instance.SelectSingleCard(this);
        }
        private void OnMouseEnter()
        {
            HighlightCard();
            transform.localScale = transform.localScale*2;
        }
        private void OnMouseExit()
        {
            if(!_isSelected)
                UnhighlightCard();
            transform.localScale = transform.localScale/2;
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
    }
}
