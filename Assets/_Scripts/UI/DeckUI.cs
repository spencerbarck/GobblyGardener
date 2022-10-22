using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SB
{
    public class DeckUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gardenDeckCountText;
        [SerializeField] private TextMeshProUGUI _spellDeckCountText;
        private void Update()
        {
            _gardenDeckCountText.text = GardenDeckManager.Instance.GetDeckSize().ToString();
            _spellDeckCountText.text = SpellDeckManager.Instance.GetDeckSize().ToString();
        }
        
    }
}
