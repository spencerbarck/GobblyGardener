using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class TurnsManager : MonoBehaviour
    {
        public static TurnsManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        private int _currentTurn = 0;
        private string _currentTurnType;
        private void Start()
        {
            _currentTurnType="Planting";
        }
        public int GetCurrentTurn()
        {
            return _currentTurn;
        }public string GetCurrentTurnType()
        {
            return _currentTurnType;
        }
        public void EndTurn()
        {
            DrawLines.Instance.HideLine();
            if(_currentTurnType=="Planting")
            {
                _currentTurnType = "Growing";
            }
            else if(_currentTurnType=="Growing")
            {
                ResourcesManager.Instance._manaCount=0;
                GridManager.Instance.HarvestGarden();
                if(_currentTurn%4==0)
                    SeasonsMananger.Instance.ChangeToNextSeason();
            }
            _currentTurn++;

            HandManager.Instance.DiscardHand();
            HandManager.Instance.ActivateHatHighlightOnSlot();
            GameManager.Instance.SetGameState(GameState.PickingHand);
            GridManager.Instance.GardenStartTurn();
            ActionRecordingMananger.Instance.ResetActionsThisTurn();
        }
    }
}
