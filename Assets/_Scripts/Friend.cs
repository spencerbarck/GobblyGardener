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
        private bool _reqOneIsComplete;
        private bool _reqTwoIsComplete;
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
            if(!_reqOneIsComplete)
            {
                if(CheckComplete(_requirementOne))
                    SetComplete(1);
            }

            if(!_reqTwoIsComplete)
            {
                if(CheckComplete(_requirementTwo))
                    SetComplete(2);
            }
        }
        private bool CheckComplete(string reqText)
        {
            var completeCheck = false;
            switch(reqText)
            {
                case "Yeild 10 Mana in a Single Turn":
                {
                    if(ResourcesManager.Instance._manaCount>=10)
                        completeCheck = true;
                    break;
                }
                case "Fill Your Garden":
                {
                    var isFull = true;
                    for(int x = 0 ; x<GridManager.Instance.GetWidth() ; x++)
                    {
                        for(int y = 0 ; y<GridManager.Instance.GetHeight() ; y++)
                        {
                            if(!GridManager.Instance.GetTileAtPosition(new Vector2(x,y))._hasCard)
                                isFull=false;
                        }
                    }
                    if(isFull)
                        completeCheck = true;
                    break;
                }
                case "Pick 15 Flowers":
                {
                    if(ResourcesManager.Instance._flowerCount>=15)
                        completeCheck = true;
                    break;
                }
                case "Grow Two Roses":
                {
                    if(GardenGrowthHistoryManager.Instance.FindCountOfCardName("Fey Roses")>1)
                        completeCheck = true;
                    break;
                }
                case "Draw 8 Cards in a Single Turn":
                {
                    if(ActionRecordingMananger.Instance.GetCardsDrawnThisTurn()>7)
                        completeCheck = true;
                    break;
                }
                case "Pick 20 Flowers and 20 Food":
                {
                    if((ResourcesManager.Instance._flowerCount>=20)&&(ResourcesManager.Instance._foodCount>=20))
                        completeCheck = true;
                    break;
                }
            }
            return completeCheck;
        }
        private void SetComplete(int req)
        {
            if(req == 1)
            {
                _requirementOneCheckbox.color = _completeColor;
                _reqOneIsComplete = true;
            }
            else
            {
                _requirementTwoCheckbox.color = _completeColor;
                _reqOneIsComplete = false;
            }
        }
    }
}
