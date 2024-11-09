using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    public GameState State;
    public int cardID = 1;

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
        UpdateGameState(GameState.PlayerTurn);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;


        switch (newState)
        {
            case GameState.DrawCard:
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.EndTurn:
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

    public enum GameState
    {
        DrawCard,
        PlayerTurn,
        EndTurn,
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