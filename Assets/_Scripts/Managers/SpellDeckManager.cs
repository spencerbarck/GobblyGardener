using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class SpellDeckManager : MonoBehaviour
    {
        public static SpellDeckManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        [SerializeField] private List<Card> _deck = new List<Card>();
        public int GetDeckSize()
        {
            return _deck.Count;
        }
        public Card PeekTopCard()
        {
            return _deck[_deck.Count-1];
        }
        public Card PeekBottomCard()
        {
            return _deck[0];
        }
        public Card PullTopCard()
        {
            var cardPulled = _deck[_deck.Count-1];
            _deck.Remove(_deck[_deck.Count-1]);
            cardPulled.gameObject.SetActive(true);
            return cardPulled;
        }
        public Card PullBottomCard()
        {
            var cardPulled = _deck[0];
            _deck.Remove(_deck[0]);
            cardPulled.gameObject.SetActive(true);
            return cardPulled;
        }
        public void PlaceCardOnTopOfDeck(Card card)
        {
            _deck.Add(card);
            card.gameObject.SetActive(false);
        }
        public void PlaceCardOnBottomOfDeck(Card card)
        {
            _deck.Insert(0,card);
            card.gameObject.SetActive(false);
        }
        public void ShuffleDeck()
        {
            var count = _deck.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var rand = UnityEngine.Random.Range(i, count);
                var tmp = _deck[i];
                _deck[i] = _deck[rand];
                _deck[rand] = tmp;
            }
        }
    }
}

