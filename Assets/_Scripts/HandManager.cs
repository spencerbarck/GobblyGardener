using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class HandManager : MonoBehaviour
    {
        public static HandManager Instance;
        [SerializeField] CardSlot[] _cardSlots;
        [SerializeField] Card _cardPrefab;
        public Card _cardSelected;
        public Dictionary<Card, int> _handDictionary;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            GenerateHand();
        }
        public void GenerateHand()
        {
            _handDictionary = new Dictionary<Card,int>();
            for(int i = 0; i<_cardSlots.Length ; i++)
            {
                if(!_cardSlots[i]._hasCard)
                    DrawCard();
            }
        }
        private bool IsInHand(Card card)
        {
            return _handDictionary.TryGetValue(card,out var cardSlotIndex);
        }
        public void SelectSingleCard(Card card)
        {
            if(IsInHand(card))
            {
                if(_cardSelected != null)
                    _cardSelected.UnSelectCard();

                _cardSelected = card;
                _cardSelected.SelectCard();
            }
        }
        public void PlaceCardSelected(Tile tile)
        {
            if(_handDictionary.TryGetValue(_cardSelected,out var cardSlotIndex))
            {
                _cardSlots[cardSlotIndex].RemoveCard();

                _handDictionary.Remove(_cardSelected);

                _cardSelected.transform.position = tile.transform.position;
                _cardSelected.UnSelectCard();
                _cardSelected = null;
            }
        }
        public void DrawCard()
        {
            if(DeckManager.Instance._deck.Count >= 1)
            {
                Card randCard = DeckManager.Instance._deck[Random.Range(0,DeckManager.Instance._deck.Count)];
                for(int i = 0; i < _cardSlots.Length; i++)
                {
                    if(!_cardSlots[i]._hasCard)
                    {
                        CardSlot cardSlot = _cardSlots[i];
                        var card = Instantiate(_cardPrefab,cardSlot.transform.position, Quaternion.identity);
                        cardSlot.AddCard(card);

                        _handDictionary[card] = i;

                        DeckManager.Instance._deck.Remove(randCard);
                        return;
                    }
                }
            }
        }
        public void DiscardCardSelected()
        {
            if(_cardSelected!= null)
            {
                if(_handDictionary.TryGetValue(_cardSelected,out var cardSlotIndex))
                {
                    _cardSlots[cardSlotIndex].RemoveCard();
                    _handDictionary.Remove(_cardSelected);

                    _cardSelected.transform.position = CompostManager.Instance.GetCompostTransform().position;
                    _cardSelected.UnSelectCard();
                    _cardSelected = null;
                }
            }
        }
        public void DiscardHand()
        {
            for(int i = 0; i < _cardSlots.Length; i++)
            {
                if(_cardSlots[i]._hasCard)
                {
                    Card card = _cardSlots[i]._storedCard;
                    _handDictionary.Remove(card);
                    card.transform.position = CompostManager.Instance.GetCompostTransform().position;
                    if(card=_cardSelected)
                    {
                        _cardSelected.UnSelectCard();
                        _cardSelected = null;
                    }
                    _cardSlots[i].RemoveCard();
                }
            }
        }
    }
}
