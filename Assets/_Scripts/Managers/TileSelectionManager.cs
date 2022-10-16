using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class TileSelectionManager : MonoBehaviour
    {
        public static TileSelectionManager Instance;
        public int _selectX;
        public int _selectY;
        public List<Tile> _tilesSlected= new List<Tile>();
        public bool _isHorizontal;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            _selectX = 1;
            _selectY = 1;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _isHorizontal=!_isHorizontal;
            }
        }
        public void RotateSelection()
        {
            if(((_selectX==1)&&(_selectY==3))||((_selectX==3)&&(_selectY==1)))
            {
                _selectX=_selectY;
                _selectY=_selectX;
            }
        }
        public void SelectTile(Tile tile)
        {
            _tilesSlected.Add(tile);
            tile.HightlightTile();
        }
        public void UnselectTiles()
        {
            foreach(Tile tile in _tilesSlected)
                tile.UnhightlightTile();
            _tilesSlected.Clear();
        }
    }
}
