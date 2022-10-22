using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class TileSelectionManager : MonoBehaviour
    {
        public static TileSelectionManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        private TileSelectionType _tileSelectionType;
        private List<Tile> _tilesSlected= new List<Tile>();
        public bool _isHorizontal;
        private void Start()
        {
            _tileSelectionType = TileSelectionType.OneXOne;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _isHorizontal=!_isHorizontal;
            }
        }
        public List<Tile> GetTilesSelected()
        {
            return _tilesSlected;
        }
        public Tile PeekFirstTileSelected()
        {
            return _tilesSlected[0];
        }
        public TileSelectionType GetTileSelectionType()
        {
            return _tileSelectionType;
        }
        public void SetTileSelectionType(TileSelectionType tileSelectionType)
        {
            _tileSelectionType = tileSelectionType;
        }
        public void SelectMultipleTiles(int tileX, int tileY)
        {
            switch(_tileSelectionType)
            {
                case TileSelectionType.OneXOne:
                {
                    SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY)));
                    break;
                }
                case TileSelectionType.TwoXOne:
                {
                    SelectTwoXOne(tileX,tileY);
                    break;
                }
                case TileSelectionType.ThreeXOne:
                {
                    SelectThreeXOne(tileX,tileY);
                    break;
                }
                case TileSelectionType.TwoXTwo:
                {
                    SelectTwoXTwo(tileX,tileY);
                    break;
                }
                case TileSelectionType.ThreeXThree:
                {
                    SelectThreeXThree();
                    break;
                }
                default: break;
            }
            
        }
        private void SelectTwoXOne(int tileX, int tileY)
        {
            if(_isHorizontal)
            {
                SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY)));
                if(GridManager.Instance.GetTileAtPosition(new Vector2(tileX+1,tileY))!=null)
                    SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX+1,tileY)));
                else
                    SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX-1,tileY)));
            }
            else
            {
                SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY)));
                if(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY+1))!=null)
                    SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY+1)));
                else
                    SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY-1)));
            }
        }
        private void SelectThreeXOne(int tileX, int tileY)
        {
            if(_isHorizontal)
            {
                for(int x = 0; x<GridManager.Instance.GetWidth(); x++)
                {
                    SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(x,tileY)));
                }
            }
            else
            {
                for(int y = 0; y<GridManager.Instance.GetHeight(); y++)
                {
                    SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,y)));
                }
            }
        }
        private void SelectTwoXTwo(int tileX, int tileY)
        {
            SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY)));
            int x;
            int y;
            if(GridManager.Instance.GetTileAtPosition(new Vector2(tileX+1,tileY))!=null)
            {
                SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX+1,tileY)));
                x=1;
            }
            else
            {
                SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX-1,tileY)));
                x=-1;
            }

            if(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY+1))!=null)
            {
                SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY+1)));
                y=1;
            }
            else
            {
                SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX,tileY-1)));
                y=-1;
            }
            SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(tileX+x,tileY+y)));
        }
        private void SelectThreeXThree()
        {
            for(int x = 0; x<GridManager.Instance.GetWidth(); x++)
            {
                for(int y = 0; y<GridManager.Instance.GetHeight(); y++)
                {
                    SelectTile(GridManager.Instance.GetTileAtPosition(new Vector2(x,y)));
                }
            }
        }
        private void SelectTile(Tile tile)
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
public enum TileSelectionType 
{
  OneXOne,
  TwoXOne,
  ThreeXOne,
  TwoXTwo,
  ThreeXThree
}
