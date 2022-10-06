using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SB
{
    public class DeckUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _deckCountText;
        private void Update()
        {
            _deckCountText.text = DeckManager.Instance._deck.Count.ToString();
        }
        
    }
}
