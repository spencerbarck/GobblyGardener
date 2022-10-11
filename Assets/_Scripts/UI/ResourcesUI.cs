using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SB
{
    public class ResourcesUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _flowerCountText;
        [SerializeField] private TextMeshProUGUI _foodCountText;
        private void Update()
        {
            _flowerCountText.text = ResourcesManager.Instance._flowerCount.ToString();
            _foodCountText.text = ResourcesManager.Instance._foodCount.ToString();
        }
    }
}
