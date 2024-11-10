using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Spine;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class C_PlayCard : MonoBehaviour, IPointerDownHandler
{
    public C_Mana manaFunction;
    public C_Health healthFunction;
    public C_Health damageFunction;
    public GameManager gameManager;

    // public Animator playCardAnimation;
    public C_CardInfo cardInfoFunction;
    private string clickedCardEffect;
    public SkeletonAnimation skeletonAnimation;
    private int randomEnemyDMG;
    private int randomPlayerDMG;

        private void Start()
    {
        gameManager = GameManager.instance;
        skeletonAnimation = GameObject.FindGameObjectWithTag("Enemy").GetComponent<SkeletonAnimation>();
        manaFunction = GameObject.FindGameObjectWithTag("Player").GetComponent<C_Mana>();
        healthFunction = GameObject.FindGameObjectWithTag("Player").GetComponent<C_Health>();
        damageFunction = GameObject.FindGameObjectWithTag("Enemy").GetComponent <C_Health>();
        cardInfoFunction = GetComponent<C_CardInfo>();
        // playCardAnimation = GetComponent<Animator>(); Suche ich aufm richtigem Objekt?


        AddPhysics2DRaycaster();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Play Card Function
        if(gameManager.playerTurn)
        {
            if (manaFunction.mana >= cardInfoFunction.cards.manaCost)
                {
                    clickedCardEffect = eventData.pointerCurrentRaycast.gameObject.GetComponent<C_CardInfo>().cards.cardName;
                    manaFunction.RemoveMana(cardInfoFunction.cards.manaCost);
                    DoCardEvents();
                    // playCardAnimation.Play("AnimationAfterCardPlayed"); Maybe play Animation?
                    Destroy(eventData.pointerCurrentRaycast.gameObject);
                }
                else 
                {
                    Debug.Log("[PlayCard] - Not Enough Mana to Play: " + eventData.pointerCurrentRaycast.gameObject.name);
                }
        }
        
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }


    #region Card Effects

    public async void DoCardEvents()
    {
        if (clickedCardEffect == "Slash") //SO Slash
        {
            Debug.Log("[PlayCard] - Playing Slash Card!");
            gameManager.cardsInHand--;
            damageFunction.RemoveHealth(2);
            skeletonAnimation.AnimationName = "being_attacked_01";
            await Task.Delay(600);
            skeletonAnimation.AnimationName = "idle";

        }
        else if (clickedCardEffect == "Broken Bottle") //SO Broken Bottle
        {
            Debug.Log("[PlayCard] - Playing Broken Bottle!");
            gameManager.cardsInHand--;
            damageFunction.RemoveHealth(4);
            skeletonAnimation.AnimationName = "being_attacked_01";
            await Task.Delay(600);
            skeletonAnimation.AnimationName = "idle";
        }
        else if (clickedCardEffect == "Barista") //SO Heal
        {
            gameManager.cardsInHand--;
            Debug.Log("[PlayCard] - Playing Barista Card");
            healthFunction.AddHealth(2);
        }
        else if (clickedCardEffect == "Punch")
        {
            gameManager.cardsInHand--;
            Debug.Log("[PlayCard] - Playing Punch");
            damageFunction.RemoveHealth(1);
            skeletonAnimation.AnimationName = "being_attacked_02";
            await Task.Delay(600);
            skeletonAnimation.AnimationName = "idle";
        }
        else if (clickedCardEffect == "Bottleflip")
        {
            gameManager.cardsInHand--;
            Debug.Log("[PlayCard] -  Playing Bottleflip");
            healthFunction.AddHealth(3);
        }
        else if (clickedCardEffect == "Five Finger Filet")
        {
            gameManager.cardsInHand--;
            Debug.Log("[PlayCard] - Playing Five Finger Filet");
            healthFunction.RemoveHealth(2);
        }
        else if (clickedCardEffect == "Hot Potato")
        {
            gameManager.cardsInHand--;
            Debug.Log("[PlayCard] - Playing Hot Potato");
            randomEnemyDMG = Random.Range(1,5);
            randomPlayerDMG = Random.Range(1,5);

            damageFunction.RemoveHealth(randomEnemyDMG);
            healthFunction.RemoveHealth(randomPlayerDMG);

            skeletonAnimation.AnimationName = "being_attacked_02";
            await Task.Delay(600);
            skeletonAnimation.AnimationName = "idle";
        }



        else
        {
            Debug.LogWarning("[PlayCard] - Can't find card in C_PlayCard, check Cards Scriptable Object or C_PlayCard if names are correctly");
        }
    }

    #endregion
}
