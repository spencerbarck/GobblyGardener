using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class ResourcesManager : MonoBehaviour
    {
        public static ResourcesManager Instance;
        public int _flowerCount;
        public int _foodCount;
        public int _manaCount;
        private void Awake()
        {
            Instance = this;
        }
        public bool CheckSpendMana(int manaCost)
        {
            if(manaCost>_manaCount)
            {
                return false;
            }
            else
            {
                _manaCount-=manaCost;
                return true;
            }
        }
    }
}
