using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class FriendRequirement : MonoBehaviour
    {
        private bool _isComplete;
        [SerializeField] private string _requirementText;
        public string GetRequirementText()
        {
            return _requirementText;
        } 
    }
}
