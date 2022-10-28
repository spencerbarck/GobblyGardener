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
        [SerializeField] private List<CardSlot> _cardSlots;
        public Card _cardSelected;
        public List<Card> _hand = new List<Card>();
        private int _maxHandSize = 5;
        private void Start()
        {       
            GardenDeckManager.Instance.ShuffleDeck();
            SpellDeckManager.Instance.ShuffleDeck();
            GenerateHand(0);
        }
        public void GenerateHand(int spellCards)
        {
            var gardenCards = _maxHandSize - spellCards;
            if((spellCards+gardenCards) >_maxHandSize)
                return;

            if(TurnsManager.Instance.GetCurrentTurnType()=="Growing")
            {
                AddCardToHandSlot(HatManager.Instance.GetCardInPlayerHat(),_cardSlots[0]);
                spellCards--;
            }

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
        public void PlaceCardSelectedInSelectedTiles()
        {
            if((_cardSelected._cardType == CardType.Garden)&&(TileSelectionManager.Instance.PeekFirstTileSelected()._hasCard))
                return;

            DrawLines.Instance.HideLine();

            if(ResourcesManager.Instance.CheckSpendMana(_cardSelected._cardManaCost)==false)
                return;

            foreach(Tile tile in TileSelectionManager.Instance.GetTilesSelected())
            {
                _cardSelected.OnPlacement(tile);
            }
            var cardTemp = _cardSelected;
            RemoveCardSelectedFromHand();
            cardTemp.OnPlay();
            TileSelectionManager.Instance.SetTileSelectionType(TileSelectionType.OneXOne);
        }
        private void RemoveCardSelectedFromHand()
        {
            foreach(CardSlot cardSlot in _cardSlots)
            {
                if(cardSlot.GetStoredCard() == _cardSelected)
                {
                    cardSlot.RemoveStoredCard();
                }
            }
            _hand.Remove(_cardSelected);
            _cardSelected.UnSelectCard();
            _cardSelected = null;
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

                        AddCardToHandSlot(cardDrawn,cardSlot);
                        ActionRecordingMananger.Instance.RecordCardDrawnThisTurn();
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
                        AddCardToHandSlot(SpellDeckManager.Instance.PullTopCard(),_cardSlots[i]);
                        ActionRecordingMananger.Instance.RecordCardDrawnThisTurn();
                        return;
                    }
                }
            }
        }
        private void AddCardToHandSlot(Card card, CardSlot cardSlot)
        {
            _hand.Add(card);
            cardSlot.StoreACard(card);
            card.transform.position = cardSlot.transform.position;
        }
        public void DiscardHand()
        {
            if(_cardSelected!=null)
            {
                _cardSelected.UnSelectCard();
                _cardSelected = null;
            }
            for(int i = 0; i < _cardSlots.Count; i++)
            {
                if(_cardSlots[i]._hasCard)
                {
                    _hand.Remove(_cardSlots[i].GetStoredCard());
                    MoveToDiscard(_cardSlots[i].GetStoredCard());
                    _cardSlots[i].RemoveStoredCard();
                }
            }
        }
        public void MoveToDiscard(Card card)
        {
            if(card._cardType == CardType.Garden)
            {
                CompostManager.Instance.AddToCompost(card);
            }
            else if(card._cardType == CardType.Spell)
            {
                SpellHistoryManager.Instance.AddToSpellHistory(card);
            }
        }
        public void ActivateHatHighlightOnSlot()
        {
            _cardSlots[0].ActivateHatHighlight();
        }
    }
}
