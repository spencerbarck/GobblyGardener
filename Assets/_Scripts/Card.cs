using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SB
{
    public class Card : MonoBehaviour
    {
        public CardType _cardType;
        public TileSelectionType _cardTileSelectionType;
        [Header("UI Settings")]
        [SerializeField] private Canvas _cardCanvas;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _manaCostText;
        [SerializeField] private Image _cardImage;
        [SerializeField] private TextMeshProUGUI _growthCostText;
        [SerializeField] private TextMeshProUGUI _rulesText;
        [SerializeField] private Image _cardWitherImage;
        [Header("Resource Settings")]
        [SerializeField] public int _cardManaCost;
        [SerializeField] public int _cardGrowthCost;
        [SerializeField] public int _foodYeild;
        [SerializeField] public int _flowerYeild;
        [SerializeField] public int _manaYeild;

        [Header("Appearance Settings")]
        [SerializeField] private Color _dryColor;
        [SerializeField] private Color _wateredColor;
        [SerializeField] private string _cardName;
        [SerializeField] private string _cardRulesText;
        [SerializeField] public Sprite _cardSprite;
        [SerializeField] private GameObject _cardBaseImage;
        [SerializeField] private GameObject _cardHighlightImage;
        [SerializeField] private SpriteRenderer _cardBackSpriteRenderer;
        [SerializeField] private GameObject _highlight;
        private bool _isSelected;
        private bool _isEnlarged;
        private bool _isWatered;
        private bool _isGrown;
        private bool _needsWater = true;
        [HideInInspector] public bool _canWither = true;
        private float _cardZValue = -0.001f;
        private int _witherCount;
        private int _maxWither = 2;
        private int _waterCount = 0;
        private int _waterCountNeeded = 1;
        [HideInInspector] public Tile _cardTile;
        private void Start()
        {
            InitCard();
        }
        private void Update()
        {
            if(_isEnlarged)
            {
                transform.position = new Vector3(transform.position.x,transform.position.y,_cardZValue-0.01f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x,transform.position.y,_cardZValue);
            }
        }
        public void InitCard()
        {
            _isGrown=false;
            _waterCount=0;
            _witherCount = 0;
            _needsWater = true;
            _canWither = true;
            UpdateCardUIElements();
            DryCard();
        }
        public void UpdateCardUIElements()
        {
            _cardBaseImage.SetActive(false);
            _cardHighlightImage.SetActive(false);
            _nameText.text = _cardName;

            _cardImage.sprite = _cardSprite;
            _rulesText.text = _cardRulesText;

            if(_witherCount>0)
                _cardWitherImage.gameObject.SetActive(true);
            else
                _cardWitherImage.gameObject.SetActive(false);

            if(_cardType==CardType.Garden)
            {
                _manaCostText.text = "";
                if(_cardGrowthCost==1)
                    _growthCostText.text = _cardGrowthCost.ToString() + " Turn To Grow";
                else
                    _growthCostText.text = _cardGrowthCost.ToString() + " Turns To Grow";
                
            }
            else
            {
                _manaCostText.text = _cardManaCost.ToString();
                _growthCostText.text = "";
            }
        }
        public string GetCardName()
        {
            return _cardName;
        }
        //
        //Card Selection / Viewing
        //
        private void OnMouseDown()
        {
            //Used if card is in the garden
            if((HandManager.Instance._cardSelected!=null)&&(HandManager.Instance._cardSelected._cardType == CardType.Spell))
            {
                if(_cardTile != null)
                {
                    HandManager.Instance.PlaceCardSelectedInSelectedTiles();
                }
            }
            
            //Movement
            if(GardenMovementManager.Instance._isChoosingCard)
            {
                GardenMovementManager.Instance.SetGardenCardToMove(this);
            }
            //

            //Used if card is in the hand
            if(!_isSelected)
                HandManager.Instance.SelectSingleCard(this);
        }
        private void OnMouseEnter()
        {
            HighlightCard();
            EnlargeCard();
            if(_cardTile!=null)
            {
                _cardTile._isHover=true;
                TileSelectionManager.Instance.SelectMultipleTiles(_cardTile._tileX,_cardTile._tileY);
            }
        }
        private void OnMouseExit()
        {
            if(!_isSelected)
                UnhighlightCard();
            UnenlargeCard();
            if(_cardTile!=null)
            {
                _cardTile._isHover=false;
                TileSelectionManager.Instance.UnselectTiles();
            }
        }
        public Card SelectCard()
        {
            _isSelected = true;
            HighlightCard();
            OnSelect();
            return this;
        }
        public void UnSelectCard()
        {
            _isSelected = false;
            UnhighlightCard();
        }
        public void HighlightCard()
        {
            _highlight.SetActive(true);
        }
        public void UnhighlightCard()
        {
            _highlight.SetActive(false);
        }
        public void EnlargeCard()
        {
            _isEnlarged = true;
            transform.localScale = transform.localScale*2;
            _cardCanvas.sortingOrder = 17;
            
            _cardBaseImage.SetActive(true);
            _cardHighlightImage.SetActive(true);
        }
        public void UnenlargeCard()
        {
            _isEnlarged = false;
            transform.localScale = transform.localScale/2;
            _cardCanvas.sortingOrder = 16;

            _cardBaseImage.SetActive(false);
            _cardHighlightImage.SetActive(false);
        }
        //
        //Card Watering / Harvesting
        //
        public void WaterCard()
        {
            OnWater();
        }
        private void DryCard()
        {
            _isWatered = false;
            _canWither = true;
            _waterCount = 0;
            if(!_needsWater)
                return;
            
            _cardBackSpriteRenderer.color = _dryColor;
            _cardBaseImage.GetComponent<Image>().color = _dryColor;
        }
        public void HarvestCard()
        {
            if((_isWatered)||(!_needsWater))
            {
                if(_cardGrowthCost>0)
                {
                    _cardGrowthCost--;
                    UpdateCardUIElements();
                }
                else
                {
                    if(!_isGrown)
                    {
                        _isGrown = true;
                        GardenGrowthHistoryManager.Instance.AddCard(this);
                    }
                    OnHarvest();
                }
            }
            else
            {
                WitherCard(1);
            }
            DryCard();
        }
        private void WitherCard(int witherAmount)
        {
            if(!_canWither)
                return;

            _witherCount += witherAmount;
            if(_witherCount>=_maxWither)
            {
                InitCard();
                _cardTile.RemoveTileCard();
                _cardTile=null;
                transform.position = CompostManager.Instance.GetCompostTransform().position;
                CompostManager.Instance.AddToCompost(this);
            }
            UpdateCardUIElements();
        }
        //
        //Card Behavior
        //
        private void OnHarvest()
        {
            ResourcesManager.Instance._flowerCount += _flowerYeild;
            ResourcesManager.Instance._foodCount += _foodYeild;
            ResourcesManager.Instance._manaCount += _manaYeild;
        }
        public void OnPlay()
        {
            switch(_cardName)
            {
                case "Diviners Boots":
                {
                    HandManager.Instance.DrawSpellCard();
                    HandManager.Instance.DrawSpellCard();
                    break;
                }
                default:
                {
                    break;
                }
            }
            if(this._cardType == CardType.Spell)
                SpellHistoryManager.Instance.AddToSpellHistory(this);
        }
        private void OnWater()
        {
            switch(_cardName)
            {
                case "Arcane Succulent":
                {
                    _needsWater = false;
                    break;
                }
                case "Greedy Dragondil":
                {
                    if(_waterCount<_waterCountNeeded)
                    {
                        _waterCount++;
                        return;
                    }
                    break;
                }
                default:
                {
                    break;
                }
            }
            _isWatered = true;
            _cardBackSpriteRenderer.color = _wateredColor;
            _cardBaseImage.GetComponent<Image>().color = _wateredColor;

            _witherCount = 0;
            UpdateCardUIElements();
        }
        public void OnPlacement(Tile tile)
        {
            if(_cardType == CardType.Garden)
            {
                tile.SetTileCard(this);
                _cardTile = tile;
                transform.position = tile.transform.position;
            }
            switch(_cardName)
            {
                case "Water Blast":
                {
                    tile.WaterCardOnTile();
                    break;
                }
                case "Magic Hose":
                {
                    tile.WaterCardOnTile();
                    break;
                }
                case "Gobbly Water Can":
                {
                    tile.WaterCardOnTile();
                    break;
                }
                case "Gardeners Miracle":
                {
                    tile.WaterCardOnTile();
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        public void OnTurnStart()
        {
            switch(_cardName)
            {
                default:
                {
                    break;
                }
            }
        }
        public void OnPick()
        {
            switch(_cardName)
            {
                case "Oracle Papyris":
                {
                    if((_isGrown)&&(GameManager.Instance.GetGameState() != GameState.PickingHand))
                        HandManager.Instance.DrawSpellCard();
                    break;
                }
                case "Clover of Youth":
                {
                    _canWither = false;
                    if(GridManager.Instance.GetAdjacentTiles(this._cardTile,-1,0)!=null)
                        if(GridManager.Instance.GetAdjacentTiles(this._cardTile,-1,0).PeekTileCard()!=null)
                            GridManager.Instance.GetAdjacentTiles(this._cardTile,-1,0).PeekTileCard()._canWither=false;
                    if(GridManager.Instance.GetAdjacentTiles(this._cardTile,1,0)!=null)
                        if(GridManager.Instance.GetAdjacentTiles(this._cardTile,1,0).PeekTileCard()!=null)
                            GridManager.Instance.GetAdjacentTiles(this._cardTile,1,0).PeekTileCard()._canWither=false;
                    if(GridManager.Instance.GetAdjacentTiles(this._cardTile,0,-1)!=null)
                        if(GridManager.Instance.GetAdjacentTiles(this._cardTile,0,-1).PeekTileCard()!=null)
                            GridManager.Instance.GetAdjacentTiles(this._cardTile,0,-1).PeekTileCard()._canWither=false;
                    if(GridManager.Instance.GetAdjacentTiles(this._cardTile,0,1)!=null)
                        if(GridManager.Instance.GetAdjacentTiles(this._cardTile,0,1).PeekTileCard()!=null)
                            GridManager.Instance.GetAdjacentTiles(this._cardTile,0,1).PeekTileCard()._canWither=false;
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        public void OnSelect()
        {
            switch(_cardName)
            {
                case "Water Blast":
                {
                    TileSelectionManager.Instance.SetTileSelectionType(_cardTileSelectionType);
                    break;
                }
                case "Magic Hose":
                {
                    TileSelectionManager.Instance.SetTileSelectionType(_cardTileSelectionType);
                    break;
                }
                case "Gobbly Water Can":
                {
                    TileSelectionManager.Instance.SetTileSelectionType(_cardTileSelectionType);
                    break;
                }
                case "Gardeners Miracle":
                {
                    TileSelectionManager.Instance.SetTileSelectionType(_cardTileSelectionType);
                    break;
                }
                case "Blink Gloves":
                {
                    GardenMovementManager.Instance._isChoosingCard=true;
                    break;
                }
                default:
                {
                    TileSelectionManager.Instance.SetTileSelectionType(_cardTileSelectionType);
                    break;
                }
            }

        }
    }
}

public enum CardType 
{
  Garden,
  Spell
}
