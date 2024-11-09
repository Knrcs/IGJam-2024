using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    public GameState State;
    public int cardID = 1; //For the Card Names
    public Cards[] cards; //Array of Scriptable Objects

    public int cardsInHand; //How many cards the player currently has in hand

    public int rngCard; // current random number of random card
    private int lastNumber;
    public GameObject cardPrefab;
    public Transform playerHand;
    public Physics2DRaycaster raycast;
    public C_Mana manaFunctions;
    public C_Bottleneck bottleneckFunctions;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (instance != null)
        { 
            Destroy(this); 
        }
        instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.DrawCard);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;


        switch (newState)
        {
            case GameState.DrawCard:
            DrawCard();
                break;
            case GameState.PlayerTurn:
            PlayerTurn();
                break;
            case GameState.OppoentsTurn:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
    public async void DrawCard()
    {
        Debug.Log("[GameState] - Draw Turn");
        for (int i = cardsInHand; i < 5; i++)
        {
            Randomize();
            await Task.Delay(120);
            Instantiate(cardPrefab, playerHand);
        } 
        UpdateGameState(GameState.PlayerTurn);
    }

    public void PlayerTurn()
    {
        raycast.enabled = true;
        manaFunctions.CheckMaxMana();
        manaFunctions.SetMaxMana();
        bottleneckFunctions.CheckBottleneck();
    }
    public void EndPlayerTurn()
    {
        UpdateGameState(GameState.OppoentsTurn);
    }
    public void Randomize()
    {
        rngCard = Random.Range(0, cards.Length);
        if (lastNumber == rngCard)
        {
        rngCard = Random.Range(0, cards.Length);

        }
        lastNumber = rngCard;
        Debug.Log("[GameManager] - " + rngCard + "is the CardID");
    }
    public enum GameState
    {
        DrawCard,
        PlayerTurn,
        OppoentsTurn,
        Win,
        Lose
    }
    public enum PlayCardEffects
    {
        SLASH,
        BROKENBOTTLE
    }
}