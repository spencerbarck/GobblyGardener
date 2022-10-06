using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        public bool _hasCard;
        public void AddCard()
        {
            _hasCard=true;
            _renderer.color = Color.blue;
        }
        public void RemoveCard()
        {
            _hasCard=false;
            Color color;
            if( ColorUtility.TryParseHtmlString("#9F846E", out color))
            {
                _renderer.color = color;
            }
        }
    }
}