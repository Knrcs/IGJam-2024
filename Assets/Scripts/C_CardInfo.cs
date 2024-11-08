using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class C_CardInfo : MonoBehaviour
{

    public Cards cards;
    public TMP_Text skillText;
    public TMP_Text descriptionText;
    public TMP_Text manaCostText;
    public Image cardImage;


    // Initialize Card
    void Start()
    {   
       cardImage = GetComponent<Image>();


        skillText.text = cards.skill;
        descriptionText.text = cards.description;
        var cardManaCost = Convert.ToString(cards.manaCost);
        manaCostText.text = cardManaCost;
        cardImage.sprite = cards.cardImage;
    }


}
