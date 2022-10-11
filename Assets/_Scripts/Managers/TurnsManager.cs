using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class TurnsManager : MonoBehaviour
    {
        public static TurnsManager Instance;
        private int _currentTurn = 0;
        private void Awake()
        {
            Instance = this;
        }
        public int GetCurrentTurn()
        {
            return _currentTurn;
        }
        public void EndTurn()
        {
            GridManager.Instance.HarvestGarden();

            HandManager.Instance.DiscardHand();
            HandManager.Instance.GenerateHand();
            _currentTurn++;
        }
    }
}
