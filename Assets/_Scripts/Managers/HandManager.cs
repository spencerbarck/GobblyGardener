using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class HandManager : MonoBehaviour
    {
        public static HandManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        [SerializeField] List<CardSlot> _cardSlots;
        [SerializeField] Card _cardPrefab;
        public Card _cardSelected;
        public List<Card> _hand = new List<Card>();
        private int _maxHandSize = 5;
        private void Start()
        {       
            GardenDeckManager.Instance.ShuffleDeck();
            SpellDeckManager.Instance.ShuffleDeck();
            GenerateHand(0,5);
        }
        public void GenerateHand(int spellCards ,int gardenCards)
        {
            if((spellCards+gardenCards) >_maxHandSize)
                return;
            for(int i = 0; i<_cardSlots.Count ; i++)
            {
                if((!_cardSlots[i]._hasCard)&&(spellCards>0))
                {
                    DrawSpellCard();
                    spellCards--;
                }
            }
            for(int i = 0; i<_cardSlots.Count ; i++)
            {
                if((!_cardSlots[i]._hasCard)&&(gardenCards>0))
                {
                    DrawGardenCard();
                    gardenCards--;
                }
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

                _cardSelected = card.SelectCard();
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

            if(_cardSelected._cardType == CardType.Garden)
            {
                tile.SetTileCard(_cardSelected);
                _cardSelected._cardTile = tile;
                _cardSelected.transform.position = tile.transform.position;
            }
            _cardSelected.OnPlacement(tile);

            _cardSelected.UnSelectCard();
            _cardSelected = null;
        }
        public void PlaceCardSelectedInSelectedTiles()
        {
            DrawLines.Instance.HideLine();

            Card cardSelected = _cardSelected;
            if(ResourcesManager.Instance.CheckSpendMana(cardSelected._cardManaCost)==false)
            {
                return;
            }

            foreach(Tile tile in TileSelectionManager.Instance._tilesSlected)
            {
                if(cardSelected._cardType == CardType.Spell)
                {
                    HandManager.Instance.PlaceCardSelected(tile);
                    HandManager.Instance._cardSelected=cardSelected;
                }
                else if(cardSelected._cardType == CardType.Garden)
                {
                    if(!tile._hasCard)
                    {
                        HandManager.Instance.PlaceCardSelected(tile);
                        HandManager.Instance._cardSelected=cardSelected;
                    }
                }
            }
            _cardSelected = null;
            
            cardSelected.OnPlay();
            if(cardSelected._cardType == CardType.Spell)
                HandManager.Instance.MoveToDiscard(cardSelected);
            
            TileSelectionManager.Instance.SetTileSelectionType(TileSelectionType.OneXOne);
        }
        public void DrawGardenCard()
        {
            if(GardenDeckManager.Instance.GetDeckSize() == 0)
                CompostManager.Instance.ReloadDeckFromCompost();

            if(GardenDeckManager.Instance.GetDeckSize() >= 1)
            {
                for(int i = 0; i < _cardSlots.Count; i++)
                {
                    if(!_cardSlots[i]._hasCard)
                    {
                        Card cardDrawn = GardenDeckManager.Instance.PullTopCard();

                        CardSlot cardSlot = _cardSlots[i];

                        cardDrawn.transform.position = cardSlot.transform.position;
                        cardSlot.AddCard(cardDrawn);
                        _hand.Add(cardDrawn);
                        return;
                    }
                }
            }
        }
        public void DrawSpellCard()
        {
            if(TurnsManager.Instance.GetCurrentTurnType()=="Planting")
                return;

            if(SpellDeckManager.Instance.GetDeckSize() == 0)
                SpellHistoryManager.Instance.ReloadDeckSpellHistory();

            if(SpellDeckManager.Instance.GetDeckSize() >= 1)
            {
                for(int i = 0; i < _cardSlots.Count; i++)
                {
                    if(!_cardSlots[i]._hasCard)
                    {
                        Card cardDrawn = SpellDeckManager.Instance.PullTopCard();

                        CardSlot cardSlot = _cardSlots[i];
                        
                        cardDrawn.transform.position = cardSlot.transform.position;
                        cardSlot.AddCard(cardDrawn);
                        _hand.Add(cardDrawn);
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
        public void MoveToDiscard(Card card)
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
