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
    }
}
