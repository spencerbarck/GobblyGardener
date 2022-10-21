using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private GameState _gameState = GameState.PlayingCards;
        private void Awake()
        {
            Instance = this;
        }
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
            foreach(Card card in GardenDeckManager.Instance._deck)
            {
                var newCard = Instantiate(card,new Vector2(0,0), Quaternion.identity);
                newCard.gameObject.SetActive(false);
                tempGardenDeck.Add(newCard);
            }
            GardenDeckManager.Instance._deck=tempGardenDeck;

            List<Card> tempSpellDeck = new List<Card>();
            foreach(Card card in SpellDeckManager.Instance._deck)
            {
                var newCard = Instantiate(card,new Vector2(0,0), Quaternion.identity);
                newCard.gameObject.SetActive(false);
                tempSpellDeck.Add(newCard);
            }
            SpellDeckManager.Instance._deck=tempSpellDeck;
        }
    }
}

public enum GameState
{
  PickingHand,
  PlayingCards
}
