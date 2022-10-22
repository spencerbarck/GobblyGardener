using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class SpellHistoryManager : MonoBehaviour
    {
        public static SpellHistoryManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        [SerializeField] private Transform _spellHistoryTransform;
        [SerializeField] private List<Card> _spellHistoryList = new List<Card>();
        private Card _topOfSpellHistory;
        public Transform GetSpellHistoryTransform()
        {
            return _spellHistoryTransform;
        }
        public void AddToSpellHistory(Card card)
        {
            card.transform.position = SpellHistoryManager.Instance.GetSpellHistoryTransform().position;
            if(_topOfSpellHistory != null)_topOfSpellHistory.gameObject.SetActive(false);
            _topOfSpellHistory=card;
            _spellHistoryList.Add(card);
        }
        public void ReloadDeckSpellHistory()
        {
            if(_topOfSpellHistory != null)_topOfSpellHistory.gameObject.SetActive(false);
            _topOfSpellHistory=null;
            
            while(_spellHistoryList.Count>0)
            {
                SpellDeckManager.Instance.PlaceCardOnTopOfDeck(_spellHistoryList[0]);
                _spellHistoryList.Remove(_spellHistoryList[0]);
            }
            SpellDeckManager.Instance.ShuffleDeck();
        }
    }
}
