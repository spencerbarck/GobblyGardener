using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class SeasonsMananger : MonoBehaviour
    {
        public static SeasonsMananger Instance;
        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] private SpriteRenderer _GardenGrassSpriteRenderer;
        [SerializeField] private Color _springColor;
        [SerializeField] private Color _summerColor;
        [SerializeField] private Color _fallColor;
        [SerializeField] private Color _winterColor;
        [SerializeField] private Season _currentSeason;
        private void Start()
        {
            SetCurrentSeason(Season.Spring);
        }
        public Season GetCurrentSeason()
        {
            return _currentSeason;
        }
        public void ChangeToNextSeason()
        {
            if(_currentSeason==Season.Spring)
                SetCurrentSeason(Season.Summer);
            else if(_currentSeason==Season.Summer)
               SetCurrentSeason(Season.Fall);
            else if(_currentSeason==Season.Fall)
                SetCurrentSeason(Season.Winter);
            else if(_currentSeason==Season.Winter)
                SetCurrentSeason(Season.Spring);
        }
        public void SetCurrentSeason(Season season)
        {
            if(season==Season.Spring)
            {
                _currentSeason = Season.Spring;
                _GardenGrassSpriteRenderer.color = _springColor;
            }
            else if(season==Season.Summer)
            {
                _currentSeason = Season.Summer;
                _GardenGrassSpriteRenderer.color = _summerColor;
            }
            else if(season==Season.Fall)
            {
                _currentSeason = Season.Fall;
                _GardenGrassSpriteRenderer.color = _fallColor;
            }
            else if(season==Season.Winter)
            {
                _currentSeason = Season.Winter;
                _GardenGrassSpriteRenderer.color = _winterColor;
            }
        }
    }
}
public enum Season
{
  Spring,
  Summer,
  Fall,
  Winter
}

