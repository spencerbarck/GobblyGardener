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
            HarvestGuarden();
            HandManager.Instance.DiscardHand();
            HandManager.Instance.GenerateHand();
            _currentTurn++;
        }
        private void HarvestGuarden()
        {
            for(int x=0;x<GridManager.Instance._width; x++)
            {
                for(int y=0;y<GridManager.Instance._height; y++)
                {
                    GridManager.Instance._tileDictionary.TryGetValue(new Vector2(x,y), out Tile tile);
                    tile.HarvestCardOnTile();
                }
            }
        }
    }
}
