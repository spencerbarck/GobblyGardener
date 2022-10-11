using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
    {
    public class CompostManager : MonoBehaviour
    {
        public static CompostManager Instance;
        [SerializeField] private Transform _compostTransform;
        private void Awake()
        {
            Instance = this;
        }
        public Transform GetCompostTransform(){ return _compostTransform;}
    }
}
