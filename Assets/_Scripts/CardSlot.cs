using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private int _cardSlotNumber;
        public bool _hasCard;
        public Card _storedCard;
        private void OnMouseEnter()
        {
            if(GameManager.Instance._gameState == GameState.PickingHand)
                _renderer.color = Color.blue;
        }
        private void OnMouseExit()
        {
            if(GameManager.Instance._gameState == GameState.PickingHand)
            {
                Color color;
                if( ColorUtility.TryParseHtmlString("#9F846E", out color))
                {
                    _renderer.color = color;
                }
            }
        }
        private void OnMouseDown()
        {
            if(GameManager.Instance._gameState == GameState.PickingHand)
            {
                HandManager.Instance.GenerateHand(_cardSlotNumber,5-_cardSlotNumber);
                GameManager.Instance._gameState = GameState.PlayingCards;
            }
        }
        public void AddCard(Card card)
        {
            _hasCard=true;
            _renderer.color = Color.blue;
            _storedCard = card;
        }
        public void RemoveCard()
        {
            _hasCard=false;
            Color color;
            if( ColorUtility.TryParseHtmlString("#9F846E", out color))
            {
                _renderer.color = color;
            }
            _storedCard = null;
        }
    }
}