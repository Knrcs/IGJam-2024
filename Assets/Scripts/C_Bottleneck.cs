using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using  UnityEngine.UI;

public class C_Bottleneck : MonoBehaviour
{
    public TMP_Text damageModifyerText;
    public int extraDamage;
    public int rdmBottleneckCards;
    public float totalBottleneckCards;
    public GameObject[] cardStack;
    public Image bottleNeckFill;
    public float bottleFillValue;
    public Transform BottleNeckDeck;
    public GameObject prefabBottleNeckCard;

    // Start is called before the first frame update
    void Start()
    {
        bottleNeckFill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckBottleneck()
    {
        GenerateCards();
        PutCardsInBottleNeckDeck();
        totalBottleneckCards += rdmBottleneckCards;
        rdmBottleneckCards = 0;
        bottleFillValue = totalBottleneckCards/10;

        Debug.Log("[BottleNeck] - Fillvalue: " + bottleFillValue);
        Debug.Log("[BottleNeck] - Total Bottleneck Cards: " + totalBottleneckCards);
        bottleNeckFill.fillAmount = bottleFillValue;

        if (totalBottleneckCards <= 5)
        {
            extraDamage = 0;
        }
        else if (totalBottleneckCards >= 7)
        {
            extraDamage = 1;
        }
        else if (totalBottleneckCards >= 9)
        {
            extraDamage = 2;
        }
        else if (totalBottleneckCards >= 10)
        {
            extraDamage = 3;
        }        
    }

    public void GenerateCards()
    {
        rdmBottleneckCards = Random.Range(1,5);
    }

    public void PutCardsInBottleNeckDeck()
    {
        for(int i = 0; i < rdmBottleneckCards; i++)
        {
            Instantiate(prefabBottleNeckCard, BottleNeckDeck);
        }
    }



}
