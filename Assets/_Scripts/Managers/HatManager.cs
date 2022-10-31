using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class HatManager : MonoBehaviour
    {
        public static HatManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        [SerializeField] private Hat _playerHat;
        public Card GetCardInPlayerHat()
        {
            return _playerHat.GetCardInHat();
        }
        public string GetPlayerHatName()
        {
            return _playerHat.GetHatName();
        }
    }
}
