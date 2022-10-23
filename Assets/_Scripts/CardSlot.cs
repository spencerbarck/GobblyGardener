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
        private Card _storedCard;
        public Card GetStoredCard()
        {
            return _storedCard;
        }
        private void OnMouseEnter()
        {
            if(GameManager.Instance.GetGameState() == GameState.PickingHand)
                _renderer.color = Color.blue;
        }
        private void OnMouseExit()
        {
            if(GameManager.Instance.GetGameState() == GameState.PickingHand)
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
            if(GameManager.Instance.GetGameState() == GameState.PickingHand)
            {
                HandManager.Instance.GenerateHand(_cardSlotNumber);
                GameManager.Instance.SetGameState(GameState.PlayingCards);
            }
        }
        public void StoreACard(Card card)
        {
            _hasCard=true;
            _renderer.color = Color.blue;
            _storedCard = card;
        }
        public void RemoveStoredCard()
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