using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SB
{
    public class GardenGrowthHistoryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _growthCountText;

        private void Update()
        {
            _growthCountText.text = "Plants Grown: "+GardenGrowthHistoryManager.Instance._growthHistory.Count.ToString();
        }
    }
}
