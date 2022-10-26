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
        [SerializeField] private Image _requirementOneCheckbox;
        [SerializeField] private Image _requirementTwoCheckbox;
        [SerializeField] private Color _completeColor;
        [SerializeField] private Color _inCompleteColor;
        [SerializeField] private string _friendName;
        [SerializeField] private Sprite _friendSprite;
        [SerializeField] private string _requirementOne;
        [SerializeField] private string _requirementTwo;
        private bool _isComplete;
        private void Start()
        {
            _friendNameText.text = _friendName;
            _friendImage.sprite =_friendSprite;
            _requirementOneText.text = _requirementOne;
            _requirementTwoText.text = _requirementTwo;
            _requirementOneCheckbox.color = _inCompleteColor;
            _requirementTwoCheckbox.color = _inCompleteColor;
        }
        public void SetCanvasCamera()
        {
            _friendCanvas.worldCamera = FindObjectOfType<Camera>();
        }
        private void Update()
        {
            if(!_isComplete)
                if(CheckComplete())
                    SetComplete();
        }
        private bool CheckComplete()
        {
            var completeCheck = false;
            switch(_friendName)
            {
                case "The Archfey":
                {
                    if(ResourcesManager.Instance._manaCount>10)
                        completeCheck = true;
                    break;
                }
                case "The King of the Goblins":
                {
                    if(GridManager.Instance.GetTileAtPosition(new Vector2(0,0))._hasCard)
                        completeCheck = true;
                    break;
                }
                case "The Rose Knight":
                {
                    if(ResourcesManager.Instance._flowerCount>5)
                        completeCheck = true;
                    break;
                }
            }
            return completeCheck;
        }
        private void SetComplete()
        {
            _requirementOneCheckbox.color = _completeColor;
            _requirementTwoCheckbox.color = _completeColor;
            _isComplete = true;
        }
    }
}
