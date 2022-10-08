using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class HandManager : MonoBehaviour
    {
        public static HandManager Instance;
        [SerializeField] List<CardSlot> _cardSlots;
        [SerializeField] Card _cardPrefab;
        public Card _cardSelected;
        public List<Card> _hand = new List<Card>();
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            DeckManager.Instance.ShuffleDeck();
            GenerateHand();
        }
        public void GenerateHand()
        {
            for(int i = 0; i<_cardSlots.Count ; i++)
            {
                if(!_cardSlots[i]._hasCard)
                    DrawCard();
            }
        }
        public void SelectSingleCard(Card card)
        {
            if(_hand.Contains(card))
            {
                if(_cardSelected != null)
                    _cardSelected.UnSelectCard();

                _cardSelected = card;
                _cardSelected.SelectCard();
            }
        }
        public void PlaceCardSelected(Tile tile)
        {
            foreach(CardSlot cardSlot in _cardSlots)
            {
                if(cardSlot._storedCard == _cardSelected)
                {
                    cardSlot.RemoveCard();
                }
            }

            _hand.Remove(_cardSelected);

            _cardSelected.transform.position = tile.transform.position;
            _cardSelected.UnSelectCard();
            _cardSelected = null;
        }
        public void DrawCard()
        {
            if(DeckManager.Instance._deck.Count >= 1)
            {
                Card randCard = DeckManager.Instance._deck[Random.Range(0,DeckManager.Instance._deck.Count)];
                for(int i = 0; i < _cardSlots.Count; i++)
                {
                    if(!_cardSlots[i]._hasCard)
                    {
                        CardSlot cardSlot = _cardSlots[i];

                        DeckManager.Instance._deck.Remove(randCard);
                        
                        var newCard = Instantiate(randCard,cardSlot.transform.position, Quaternion.identity);
                        cardSlot.AddCard(newCard);

                        _hand.Add(newCard);

                        return;
                    }
                }
            }
        }
        public void DiscardCardSelected()
        {
            if(_cardSelected!= null)
            {
                foreach(CardSlot cardSlot in _cardSlots)
                {
                    if(cardSlot._storedCard == _cardSelected)
                    {
                        cardSlot.RemoveCard();
                    }
                }
                _hand.Remove(_cardSelected);

                _cardSelected.transform.position = CompostManager.Instance.GetCompostTransform().position;
                _cardSelected.UnSelectCard();
                _cardSelected = null;
            }
        }
        public void DiscardHand()
        {
            for(int i = 0; i < _cardSlots.Count; i++)
            {
                if(_cardSlots[i]._hasCard)
                {
                    Card card = _cardSlots[i]._storedCard;
                    _hand.Remove(card);
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
