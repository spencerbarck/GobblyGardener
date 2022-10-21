using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SB
{
    public class GardenMovementManager : MonoBehaviour
    {
        public static GardenMovementManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        private Card _cardMoving;
        public bool _isChoosingCard;
        public bool _isMoving;
        public void SetGardenCardToMove(Card card)
        {
            _cardMoving = card;
            _isChoosingCard = false;
            _isMoving = true;
        }
        public void PlaceGardenCardToMove(Tile tile)
        {
            _cardMoving._cardTile.RemoveTileCard();
            tile.SetTileCard(_cardMoving);
            _cardMoving._cardTile = tile;

            _cardMoving.transform.position = tile.transform.position;

            _cardMoving=null;
            _isMoving = false;
        }
    }
}

