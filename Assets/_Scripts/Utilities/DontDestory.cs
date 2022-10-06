using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    
    public class DontDestory : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
