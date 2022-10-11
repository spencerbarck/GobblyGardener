using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SB
{
    public class TileDetailsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tileValueText;
        [SerializeField] private TextMeshProUGUI _tileColorText;

        private void Update()
        {
            if(GridManager.Instance._tileInFocus!=null)
            {
                _tileValueText.text = GridManager.Instance._tileInFocus._tileValue.ToString();
                _tileColorText.text = GridManager.Instance._tileInFocus.GetSpriteRenderer().color.ToString();
            }
            else
            {
                _tileValueText.text = "";
                _tileColorText.text = "";
            }
        }
        
    }
}
