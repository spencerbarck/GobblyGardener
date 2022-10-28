using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    
    public class ActionRecordingMananger : MonoBehaviour
    {
        public static ActionRecordingMananger Instance;
        private void Awake()
        {
            Instance = this;
        }
        private int _cardsDrawnThisTurn;
        public int GetCardsDrawnThisTurn()
        {
            return _cardsDrawnThisTurn;
        }
        public void RecordCardDrawnThisTurn()
        {
            _cardsDrawnThisTurn += 1;
        }
        public void ResetActionsThisTurn()
        {
            _cardsDrawnThisTurn = 0;
        }
    }
}