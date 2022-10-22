using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        private GameState _gameState = GameState.PlayingCards;
        private void Start()
        {
            InstantiateAllCards();
        }
        public GameState GetGameState()
        {
            return _gameState;
        }
        public void SetGameState(GameState gameState)
        {
            _gameState = gameState;
        }
        private void InstantiateAllCards()
        {
            List<Card> tempGardenDeck = new List<Card>();
            while(GardenDeckManager.Instance.GetDeckSize()>0)
            {
                var newCard = Instantiate(GardenDeckManager.Instance.PullTopCard(),new Vector2(0,0), Quaternion.identity);
                tempGardenDeck.Add(newCard);
            }
            while(tempGardenDeck.Count>0)
            {
                GardenDeckManager.Instance.PlaceCardOnTopOfDeck(tempGardenDeck[0]);
                tempGardenDeck.Remove(tempGardenDeck[0]);
            }

            List<Card> tempSpellDeck = new List<Card>();
            while(SpellDeckManager.Instance.GetDeckSize()>0)
            {
                var newCard = Instantiate(SpellDeckManager.Instance.PullTopCard(),new Vector2(0,0), Quaternion.identity);
                tempSpellDeck.Add(newCard);
            }
            while(tempSpellDeck.Count>0)
            {
                SpellDeckManager.Instance.PlaceCardOnTopOfDeck(tempSpellDeck[0]);
                tempSpellDeck.Remove(tempSpellDeck[0]);
            }
        }
    }
}

public enum GameState
{
  PickingHand,
  PlayingCards
}
