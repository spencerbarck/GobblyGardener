using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SB
{
    public class Friend : MonoBehaviour
    {
        [SerializeField] private Canvas _friendCanvas;
        [SerializeField] private TextMeshProUGUI _friendNameText;
        [SerializeField] private Image _friendImage;
        [SerializeField] private TextMeshProUGUI _requirementOneText;
        [SerializeField] private TextMeshProUGUI _requirementTwoText;
        [SerializeField] private string _friendName;
        [SerializeField] private Sprite _friendSprite;
        [SerializeField] private string _requirementOne;
        [SerializeField] private string _requirementTwo;
        private void Start()
        {
            _friendNameText.text = _friendName;
            _friendImage.sprite =_friendSprite;
            _requirementOneText.text = _requirementOne;
            _requirementTwoText.text = _requirementTwo;
        }
        public void SetCanvasCamera()
        {
            _friendCanvas.worldCamera = FindObjectOfType<Camera>();
        }
    }
}
