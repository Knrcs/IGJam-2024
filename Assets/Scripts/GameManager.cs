using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    public GameState State;

    //BOSSTURN
    private int randomPlayingCard;
    public int bossRotation = 2;

    public GameObject pirateBoss;
    public GameObject seaCreatureBoss;
    public GameObject CorkBoss;



    public int cardID = 1; //For the Card Names
    public Cards[] cards; //Array of Scriptable Objects

    public int cardsInHand; //How many cards the player currently has in hand

    public int rngCard; // current random number of random card
    private int lastNumber;
    public GameObject cardPrefab;
    public Transform playerHand;
    public Physics2DRaycaster raycast;
    public C_Mana manaFunctions;
    public C_Mana bossManaFunctions;
    public C_Health bossDoDamage;
    public C_Health bossHealth;
    public SkeletonAnimation bossAnimation;
    public C_Bottleneck bottleneckFunctions;
    public GameObject endTurnButton;
    public bool playerTurn;
    public GameObject loseScreen;
    public bool canEnemyplay;
    public SkeletonAnimation[] skeletonAssets;
    public C_PlayCard playCard;

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
                if(canEnemyplay == false)
                {
                    UpdateGameState(GameState.DrawCard);
                }
                if(canEnemyplay == true)
                {
                    OppoentsTurn();
                }
                break;
            case GameState.Win:
            Win();
                break;
            case GameState.Lose:
                LoseGame();
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
            cardsInHand++;
            Randomize();
            await Task.Delay(120);
            Instantiate(cardPrefab, playerHand);
            bottleneckFunctions.RemoveOneFromBottleneck();

        } 
        Debug.Log("Warum machst du so");
        canEnemyplay = true;
        UpdateGameState(GameState.PlayerTurn);
    }

    public void PlayerTurn()
    {
        playCard.CheckBoss();
        playerTurn = true;
        manaFunctions.CheckMaxMana();
        manaFunctions.SetMaxMana();
        bottleneckFunctions.CheckBottleneck();
        endTurnButton.SetActive(true);
    }
    public void EndPlayerTurn()
    {
        playerTurn = false;
        endTurnButton.SetActive(false);
        UpdateGameState(GameState.OppoentsTurn);
    }

    public async void OppoentsTurn()
    {
        bossManaFunctions.CheckMaxMana();
        bossManaFunctions.SetMaxMana();
        await Task.Delay(1000);

        for(int i = 0; i < 5; i++) //5 tries to play cards
        {
            randomPlayingCard = Random.Range(1,4);
            switch(randomPlayingCard)
            {
                case 1: //Slash
                    if(bossManaFunctions.mana >= 2)
                    {
                        bossManaFunctions.RemoveMana(2);
                        bossDoDamage.RemoveHealth(2 + bottleneckFunctions.extraDamage);
                        bossAnimation.AnimationName = "attacking_01";
                        await Task.Delay(600);
                        bossAnimation.AnimationName = "idle";
                        await Task.Delay(700);
                        CheckHealth();

                    }
                    else break;
                break;
                case 2: //Broken Bottle
                    if(bossManaFunctions.mana >= 3)
                    {
                        bossManaFunctions.RemoveMana(3);
                        bossDoDamage.RemoveHealth(4 + bottleneckFunctions.extraDamage);
                        bossAnimation.AnimationName = "attacking_02";
                        await Task.Delay(600);
                        bossAnimation.AnimationName = "idle";
                        await Task.Delay(700);
                        CheckHealth();
                    }
                    else break;
                break;
                case 3: //Punch
                    if(bossManaFunctions.mana >= 1)
                    {
                        bossManaFunctions.RemoveMana(1);
                        bossDoDamage.RemoveHealth(1 + bottleneckFunctions.extraDamage);
                        bossAnimation.AnimationName = "attacking_01";
                        await Task.Delay(600);
                        bossAnimation.AnimationName = "idle";
                        await Task.Delay(700);
                        CheckHealth();

                    }
                    else break;
                break;
                case 4: //Barista
                    if(bossManaFunctions.mana <= 2)
                    {
                        bossManaFunctions.RemoveMana(2);
                        bossHealth.AddHealth(2);
                        bossAnimation.AnimationName = "attacking_01";
                        await Task.Delay(600);
                        bossAnimation.AnimationName = "idle";
                        await Task.Delay(700);
                        CheckHealth();
                    } 
                    else break;
                break;
            }
            Debug.Log("Boss Turn: " + i);
        }
        UpdateGameState(GameState.DrawCard);
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

    public void LoseGame()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;


    }

    public void CheckHealth()
    {
        if (bossDoDamage.currentHealth <= 0)
        {
            UpdateGameState(GameState.Lose);
        }
        if (bossHealth.currentHealth <= 0)
        {
            UpdateGameState(GameState.Win);
        }
    }

    public void Win()
    {
        if(bossRotation == 1)
        {
            bossAnimation = skeletonAssets[0];
            seaCreatureBoss.SetActive(false);
            pirateBoss.SetActive(true);
            CorkBoss.SetActive(false);
            bossRotation = 2;
        }
        else if(bossRotation == 2)
        {
            bossAnimation = skeletonAssets[1];
            seaCreatureBoss.SetActive(true);
            pirateBoss.SetActive(false);
            CorkBoss.SetActive(false);

            bossRotation = 3;
        }
        else if(bossRotation == 3)
        {
            bossAnimation = skeletonAssets[2];
            seaCreatureBoss.SetActive(false);
            pirateBoss.SetActive(false);
            CorkBoss.SetActive(true);

            bossRotation = 1;
        }
        bossHealth.currentHealth = 0;
        bossHealth.healthBonus = bossHealth.healthBonus + 3;
        bossHealth.currentHealth = 10 + bossHealth.healthBonus;
        bossHealth.healthText.text = bossHealth.currentHealth.ToString();
        manaFunctions.NewRoundMana(2);
        bossManaFunctions.NewRoundMana(1);
        UpdateGameState(GameState.DrawCard);
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