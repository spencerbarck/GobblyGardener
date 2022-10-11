using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance;
        [SerializeField] private Transform _parent;
        [SerializeField] public int _width, _height;
        [SerializeField] private float _startXPos, _startYPos, _xTileShift, _yTileShift;
        [SerializeField] private Tile _tilePrefab;
        public Dictionary<Vector2, Tile> _tileDictionary;
        public Tile _tileInFocus;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            _tileDictionary = new Dictionary<Vector2, Tile>();
            Vector3 tilePosition = new Vector3(_startXPos,_startYPos);
            for (int x = 0; x < _width; x++)
            {
                for(int y = 0; y < _height; y++)
                {
                    var spawnedTile = Instantiate(_tilePrefab, tilePosition,Quaternion.identity,_parent);
                    spawnedTile.name = $"Tile {y} {x}";

                    var isOffset = (x % 2 == 0 && y % 2 != 0)||(x % 2 != 0 && y % 2 == 0);
                    spawnedTile.Init(isOffset);
                    tilePosition.y += _yTileShift;

                    _tileDictionary[new Vector2(x,y)] = spawnedTile;
                }
                tilePosition.y = _startYPos;
                tilePosition.x += _xTileShift;
            }
        }
        public Tile GetTileAtPosition(Vector2 pos)
        {
            if(_tileDictionary.TryGetValue(pos,out var Tile))
            {
                return Tile;
            }
            return null;
        }
        public void FocusTile(Tile tile)
        {
            _tileInFocus = tile;
        }
        public void UnFocusTile()
        {
            _tileInFocus = null;
        }
        public void HarvestGarden()
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