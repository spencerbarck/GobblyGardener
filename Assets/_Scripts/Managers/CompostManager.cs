using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
    {
    public class CompostManager : MonoBehaviour
    {
        public static CompostManager Instance;
        [SerializeField] private Transform _compostTransform;
        [SerializeField] private List<Card> _compostList = new List<Card>();
        private Card _topOfCompost;
        private void Awake()
        {
            Instance = this;
        }
        public Transform GetCompostTransform()
        {
             return _compostTransform;
        }
        public void AddToCompost(Card card)
        {
            if(_topOfCompost != null)_topOfCompost.gameObject.SetActive(false);
            _topOfCompost=card;
            _compostList.Add(card);
        }
        public void ReloadDeckFromCompost()
        {
            if(_topOfCompost != null)_topOfCompost.gameObject.SetActive(false);
            _topOfCompost=null;

            while(_compostList.Count>0)
            {
                _compostList[0].gameObject.SetActive(false);
                GardenDeckManager.Instance._deck.Add(_compostList[0]);
                _compostList.Remove(_compostList[0]);
            }
            GardenDeckManager.Instance.ShuffleDeck();
        }
    }
}
