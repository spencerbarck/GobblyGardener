using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class GardenGrowthHistoryManager : MonoBehaviour
    {
        public static GardenGrowthHistoryManager Instance;
        [SerializeField] private Transform _growthHistoryTransform;
        private List<Card> _growthHistory = new List<Card>();
        private void Awake()
        {
            Instance = this;
        }
        public int GetHistorySize()
        {
            return _growthHistory.Count;
        }
        public void AddCard(Card card)
        {
            var cardInHistory = Instantiate(card);
            cardInHistory.transform.position = _growthHistoryTransform.position;
            cardInHistory.transform.localScale = new Vector2(cardInHistory.transform.localScale.x * 0.75f,cardInHistory.transform.localScale.y * 0.75f);
            cardInHistory.InitCard();
            _growthHistory.Add(cardInHistory);
            _growthHistoryTransform.Translate(new Vector3(0,-1.33f,0));
        }
    }
}
