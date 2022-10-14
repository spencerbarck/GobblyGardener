using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SB
{
    public class TurnUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentTurnText;
        [SerializeField] private TextMeshProUGUI _currentTurnTypeText;
        private void Update()
        {
            _currentTurnText.text = TurnsManager.Instance.GetCurrentTurn().ToString();
            _currentTurnTypeText.text = TurnsManager.Instance.GetCurrentTurnType();
        }
    }
}
