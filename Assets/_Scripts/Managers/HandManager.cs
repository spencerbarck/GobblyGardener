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
            InstantiateAllCards();    
            GardenDeckManager.Instance.ShuffleDeck();
            GenerateHand("Garden");
        }
        private void InstantiateAllCards()
        {
            List<Card> tempGardenDeck = new List<Card>();
            foreach(Card card in GardenDeckManager.Instance._deck)
            {
                var newCard = Instantiate(card,new Vector2(0,0), Quaternion.identity);
                newCard.gameObject.SetActive(false);
                tempGardenDeck.Add(newCard);
            }
            GardenDeckManager.Instance._deck=tempGardenDeck;

            List<Card> tempSpellDeck = new List<Card>();
            foreach(Card card in SpellDeckManager.Instance._deck)
            {
                var newCard = Instantiate(card,new Vector2(0,0), Quaternion.identity);
                newCard.gameObject.SetActive(false);
                tempSpellDeck.Add(newCard);
            }
            SpellDeckManager.Instance._deck=tempSpellDeck;
        }
        public void GenerateHand(string cardType)
        {
            for(int i = 0; i<_cardSlots.Count ; i++)
            {
                if(!_cardSlots[i]._hasCard)
                    if(cardType == "Garden")DrawGardenCard();
                    if(cardType == "Spell")DrawSpellCard();
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

            if(_cardSelected._cardManaCost>ResourcesManager.Instance._manaCount)
            {
                return;
            }
            else
            {
                ResourcesManager.Instance._manaCount -= _cardSelected._cardManaCost;
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
            if(GardenDeckManager.Instance._deck.Count == 0)
                CompostManager.Instance.ReloadDeckFromCompost();

            if(GardenDeckManager.Instance._deck.Count >= 1)
            {
                Card randCard = GardenDeckManager.Instance._deck[Random.Range(0,GardenDeckManager.Instance._deck.Count)];
                for(int i = 0; i < _cardSlots.Count; i++)
                {
                    if(!_cardSlots[i]._hasCard)
                    {
                        CardSlot cardSlot = _cardSlots[i];

                        GardenDeckManager.Instance._deck.Remove(randCard);

                        randCard.transform.position = cardSlot.transform.position;
                        cardSlot.AddCard(randCard);
                        _hand.Add(randCard);

                        randCard.gameObject.SetActive(true);
                        return;
                    }
                }
            }
        }
        public void DrawSpellCard()
        {
            if(TurnsManager.Instance.GetCurrentTurnType()=="Planting")
                return;

            if(SpellDeckManager.Instance._deck.Count == 0)
                SpellHistoryManager.Instance.ReloadDeckSpellHistory();

            if(SpellDeckManager.Instance._deck.Count >= 1)
            {
                Card randCard = SpellDeckManager.Instance._deck[Random.Range(0,SpellDeckManager.Instance._deck.Count)];
                for(int i = 0; i < _cardSlots.Count; i++)
                {
                    if(!_cardSlots[i]._hasCard)
                    {
                        CardSlot cardSlot = _cardSlots[i];

                        SpellDeckManager.Instance._deck.Remove(randCard);
                        
                        randCard.transform.position = cardSlot.transform.position;
                        cardSlot.AddCard(randCard);

                        _hand.Add(randCard);

                        randCard.gameObject.SetActive(true);
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

                MoveToDiscard(_cardSelected);

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
                    
                    MoveToDiscard(card);
                    
                    if(card==_cardSelected)
                    {
                        _cardSelected.UnSelectCard();
                        _cardSelected = null;
                    }
                    _cardSlots[i].RemoveCard();
                }
            }
        }
        private void MoveToDiscard(Card card)
        {
            if(card._cardType == CardType.Garden)
            {
                card.transform.position = CompostManager.Instance.GetCompostTransform().position;
                CompostManager.Instance.AddToCompost(card);
            }
            else if(card._cardType == CardType.Spell)
            {
                card.transform.position = SpellHistoryManager.Instance.GetSpellHistoryTransform().position;
                SpellHistoryManager.Instance.AddToSpellHistory(card);
            }
        }
    }
}
