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

    public C_CardInfo cardInfoFunction;
    private string clickedCardEffect;
    public SkeletonAnimation skeletonAnimation;

        private void Start()
    {
        skeletonAnimation = GameObject.FindGameObjectWithTag("Enemy").GetComponent<SkeletonAnimation>();
        manaFunction = GameObject.FindGameObjectWithTag("Player").GetComponent<C_Mana>();
        healthFunction = GameObject.FindGameObjectWithTag("Player").GetComponent<C_Health>();
        damageFunction = GameObject.FindGameObjectWithTag("Enemy").GetComponent <C_Health>();
        cardInfoFunction = GetComponent<C_CardInfo>();
        AddPhysics2DRaycaster();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Play Card Function
        if (manaFunction.mana >= cardInfoFunction.cards.manaCost)
        {
            clickedCardEffect = eventData.pointerCurrentRaycast.gameObject.GetComponent<C_CardInfo>().cards.cardName;
            manaFunction.RemoveMana(cardInfoFunction.cards.manaCost);
            DoCardEvents();
            Destroy(eventData.pointerCurrentRaycast.gameObject);
        }
        else 
        {
            Debug.Log("[PlayCard] - Not Enough Mana to Play: " + eventData.pointerCurrentRaycast.gameObject.name);
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
            damageFunction.RemoveHealth(2);
            skeletonAnimation.AnimationName = "attacking_01";
            await Task.Delay(250);
            skeletonAnimation.AnimationName = "idle";

        }
        else if (clickedCardEffect == "Broken Bottle") //SO Broken Bottle
        {
            Debug.Log("[PlayCard] - Playing Broken Bottle!");
            damageFunction.RemoveHealth(4);
        }
        else if (clickedCardEffect == "Barista") //SO Heal
        {
            Debug.Log("[PlayCard] - Playing Barista Card");
            healthFunction.AddHealth(2);
        }
        else if (clickedCardEffect == "Punch")
        {
            Debug.Log("[PlayCard] - Playing Punch");
            damageFunction.RemoveHealth(1);
        }
        else if (clickedCardEffect == "Bottleflip")
        {
            Debug.Log("[PlayCard] -  Playing Bottleflip");
        }


        else
        {
            Debug.LogWarning("[PlayCard] - Can't find card in C_PlayCard, check Cards Scriptable Object or C_PlayCard if names are correctly");
        }
    }

    #endregion
}
