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
        private void Awake()
        {
            Instance = this;
        }
    }
}
