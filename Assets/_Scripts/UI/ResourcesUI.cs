using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SB
{
    public class ResourcesUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _flowerCountText;
        private void Update()
        {
            _flowerCountText.text = ResourcesManager.Instance._flowerCount.ToString();
        }
    }
}
