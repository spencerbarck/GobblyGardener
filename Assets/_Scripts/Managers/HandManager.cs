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
            GardenDeckManager.Instance.ShuffleDeck();
            GenerateHand();
        }
        public void GenerateHand()
        {
            for(int i = 0; i<_cardSlots.Count ; i++)
            {
                if(!_cardSlots[i]._hasCard)
                    DrawGardenCard();
            }
        }
        public void SelectSingleCard(Card card)
        {
            if(_hand.Contains(card))
            {
                DrawLines.Instance._startLinePosition = card.transform.position;
                DrawLines.Instance._isDrawingLine = true;

                if(_cardSelected != null)
                    _cardSelected.UnSelectCard();

                _cardSelected = card;
                _cardSelected.SelectCard();
            }
        }
        public void PlaceCardSelected(Tile tile)
        {
            if((_cardSelected._cardType == CardType.Spell)&&(!tile._hasCard))
            {
                return;
            }

            DrawLines.Instance.HideLine();
            foreach(CardSlot cardSlot in _cardSlots)
            {
                if(cardSlot._storedCard == _cardSelected)
                {
                    cardSlot.RemoveCard();
                }
            }
            _hand.Remove(_cardSelected);

            if(_cardSelected._cardType == CardType.Garden)
            {
                tile.SetTileCard(_cardSelected);
                _cardSelected._cardTile = tile;
                _cardSelected.transform.position = tile.transform.position;
            }
            else if(_cardSelected._cardType == CardType.Spell)
            {
                tile.WaterCardOnTile();
                _cardSelected.transform.position = SpellHistoryManager.Instance.GetSpellHistoryTransform().position;
            }

            _cardSelected.UnSelectCard();
            _cardSelected = null;
        }
        public void DrawGardenCard()
        {
            if(GardenDeckManager.Instance._deck.Count >= 1)
            {
                Card randCard = GardenDeckManager.Instance._deck[Random.Range(0,GardenDeckManager.Instance._deck.Count)];
                for(int i = 0; i < _cardSlots.Count; i++)
                {
                    if(!_cardSlots[i]._hasCard)
                    {
                        CardSlot cardSlot = _cardSlots[i];

                        GardenDeckManager.Instance._deck.Remove(randCard);
                        
                        var newCard = Instantiate(randCard,cardSlot.transform.position, Quaternion.identity);
                        cardSlot.AddCard(newCard);

                        _hand.Add(newCard);

                        return;
                    }
                }
            }
        }
        public void DrawSpellCard()
        {
            if(SpellDeckManager.Instance._deck.Count >= 1)
            {
                Card randCard = SpellDeckManager.Instance._deck[Random.Range(0,SpellDeckManager.Instance._deck.Count)];
                for(int i = 0; i < _cardSlots.Count; i++)
                {
                    if(!_cardSlots[i]._hasCard)
                    {
                        CardSlot cardSlot = _cardSlots[i];

                        SpellDeckManager.Instance._deck.Remove(randCard);
                        
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

                if(_cardSelected._cardType == CardType.Garden)
                {
                    _cardSelected.transform.position = CompostManager.Instance.GetCompostTransform().position;
                }
                else if(_cardSelected._cardType == CardType.Spell)
                {
                    _cardSelected.transform.position = SpellHistoryManager.Instance.GetSpellHistoryTransform().position;
                }
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
                    
                    if(card._cardType == CardType.Garden)
                    {
                        card.transform.position = CompostManager.Instance.GetCompostTransform().position;
                    }
                    else if(card._cardType == CardType.Spell)
                    {
                        card.transform.position = SpellHistoryManager.Instance.GetSpellHistoryTransform().position;
                    }
                    if(card==_cardSelected)
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
