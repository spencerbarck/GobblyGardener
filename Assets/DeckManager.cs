using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class DeckManager : MonoBehaviour
    {
        public static DeckManager Instance;
        public List<Card> _deck = new List<Card>();
        private void Awake()
        {
            Instance = this;
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
