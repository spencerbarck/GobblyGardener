using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class SpellHistoryManager : MonoBehaviour
    {
        public static SpellHistoryManager Instance;
        [SerializeField] private Transform _spellHistoryTransform;
        private void Awake()
        {
            Instance = this;
        }
        public Transform GetSpellHistoryTransform(){ return _spellHistoryTransform;}
    }
}
