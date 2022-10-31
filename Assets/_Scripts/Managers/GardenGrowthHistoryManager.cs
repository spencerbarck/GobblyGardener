using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class GardenGrowthHistoryManager : MonoBehaviour
    {
        public static GardenGrowthHistoryManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        [SerializeField] private Transform _growthHistoryTransformStart;
        private Transform _growthHistoryTransform;
        private List<Card> _growthHistory = new List<Card>();
        private void Start()
        {
            _growthHistoryTransform = _growthHistoryTransformStart;
        }
        public int GetHistorySize()
        {
            return _growthHistory.Count;
        }
        public void AddCard(Card card)
        {
            var cardInHistory = Instantiate(card);
            cardInHistory.transform.position = _growthHistoryTransform.position;
            cardInHistory.transform.localScale = new Vector2(cardInHistory.transform.localScale.x * 0.666f,cardInHistory.transform.localScale.y * 0.75f);
            cardInHistory.InitCard();
            _growthHistory.Add(cardInHistory);

            MoveTransform();
        }
        private void MoveTransform()
        {
            if(_growthHistory.Count % 3 == 0)
            {
                _growthHistoryTransform.Translate(new Vector3(1.333f,2.666f,0));
            }
            else
            _growthHistoryTransform.Translate(new Vector3(0,-1.333f,0));
        }
        public int FindCountOfCardName(string name)
        {
            var count = 0;
            foreach(Card card in _growthHistory)
            {
                if(card.GetCardName()==name)
                    count++;
            }
            return count;
        }
    }
}
