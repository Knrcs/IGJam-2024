using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class C_CardInfo : MonoBehaviour
{

    [HideInInspector] public GameManager gameManager;
    public Cards cards;
    public TMP_Text cardNameText;
    public TMP_Text skillText;
    public TMP_Text descriptionText;
    public TMP_Text manaCostText;
    public Image cardImage;
    public new BoxCollider2D collider;


    // Initialize Card
    void Start()
    {   
        collider = GetComponent<BoxCollider2D>();
        gameManager = GameManager.instance;
        gameObject.name = cards.cardName + gameManager.cardID.ToString("D4");
        gameManager.cardID++;
        //Puts Scriptable Object Variables into UI Card
        cardImage = GetComponent<Image>();
        cardNameText.text = cards.cardName;
        skillText.text = cards.skill;
        descriptionText.text = cards.description;
        var cardManaCost = cards.manaCost.ToString();
        manaCostText.text = cardManaCost;
        cardImage.sprite = cards.cardImage;
    }




}
