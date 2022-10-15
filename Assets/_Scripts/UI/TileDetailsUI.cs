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
            if(TileSelectionManager.Instance._tilesSlected.Count!=0)
            {
                string locationString = "";
                locationString+= TileSelectionManager.Instance._tilesSlected[0]._tileX;
                locationString+=" ";
                locationString+= TileSelectionManager.Instance._tilesSlected[0]._tileY;
                _tileValueText.text = locationString;
                _tileColorText.text = TileSelectionManager.Instance._tilesSlected[0].GetSpriteRenderer().color.ToString();
            }
            else
            {
                _tileValueText.text = "";
                _tileColorText.text = "";
            }
        }
        
    }
}
