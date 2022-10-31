using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class Hat : MonoBehaviour
    {
        [SerializeField] private Card _cardInHat;
        [SerializeField] private string _hatName;
        public Card GetCardInHat()
        {
            var cardInstantiated = Instantiate(_cardInHat);
            return cardInstantiated;
        }
        public string GetHatName()
        {
            return _hatName;
        }
    }
}
