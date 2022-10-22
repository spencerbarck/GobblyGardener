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
            if(TileSelectionManager.Instance.GetTilesSelected().Count!=0)
            {
                string locationString = "";
                locationString+= TileSelectionManager.Instance.PeekFirstTileSelected()._tileX;
                locationString+=" ";
                locationString+= TileSelectionManager.Instance.PeekFirstTileSelected()._tileY;
                _tileValueText.text = locationString;
                _tileColorText.text = TileSelectionManager.Instance.PeekFirstTileSelected().GetSpriteRenderer().color.ToString();
            }
            else
            {
                _tileValueText.text = "";
                _tileColorText.text = "";
            }
        }
        
    }
}
