using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SB
{
    public class TurnUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentTurnText;
        private void Update()
        {
            _currentTurnText.text = TurnsManager.Instance.GetCurrentTurn().ToString();
        }
    }
}
