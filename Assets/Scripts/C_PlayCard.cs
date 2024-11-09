using System.Collections;
using System.Collections.Generic;
using Spine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class C_PlayCard : MonoBehaviour, IPointerDownHandler
{
    public C_Mana manaFunction;
    public C_Health healthFunction;
    public C_CardInfo cardInfoFunction;
    private string clickedCardEffect;

        private void Start()
    {
        manaFunction = GameObject.FindGameObjectWithTag("Player").GetComponent<C_Mana>();
        healthFunction = GameObject.FindGameObjectWithTag("Player").GetComponent<C_Health>();
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
            Debug.Log("Not Enough Mana to Play: " + eventData.pointerCurrentRaycast.gameObject.name);
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

    public void DoCardEvents()
    {
        if (clickedCardEffect == "Slash")
        {
            Debug.Log("Slash Card!");
        }
        else if (clickedCardEffect == "Broken Bottle")
        {
            Debug.Log("Broken Bottle!");
        }


        else
        {
            Debug.LogWarning("Can't find card in C_PlayCard, check Cards Scriptable Object or C_PlayCard if names are correctly");
        }
    }

    #endregion
}
