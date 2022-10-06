using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private GameObject _highlight;
        private bool _isSelected;
        private void OnMouseDown()
        {
            if(!_isSelected)
                //transform.position = CompostManager.Instance.GetCompostTransform().position;
                HandManager.Instance.SelectSingleCard(this);
            
        }
        private void OnMouseEnter()
        {
            HighlightCard();
        }
        private void OnMouseExit()
        {
            if(!_isSelected)
                UnhighlightCard();
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
