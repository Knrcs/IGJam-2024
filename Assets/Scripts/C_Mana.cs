using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class C_Mana : MonoBehaviour
{

    public int mana;
    private int maxMana;
    public GameObject[] manaBlibs;
    public C_ManaVisual manaVisual;

    // Start is called before the first frame update
    void Start()
    {
        maxMana = mana;
        SetMaxMana();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckMaxMana()
    {
        maxMana = maxMana+1;
        if (maxMana >= 10)
        {
            maxMana = 10;
            Debug.Log("Max Mana reached");
        }
    }

    public void AddMana(int m)
    {
        if(mana < maxMana)
        {
            mana = mana + m;
            for(int i = 0; i < manaBlibs.Length; i++)
            {
                manaBlibs[i].SetActive(false);
            }

            for(int i = 0; i < mana; i++)
            {
                manaBlibs[i].SetActive(true);
            }
        }
        else return;
    }
    public void SetMaxMana()
    {
        mana = maxMana;
        for(int i = 0; i < mana; i++)
        {
            manaBlibs[i].SetActive(true);
        } 
    }
    public void NewRoundMana(int i)
    {
        maxMana = i;
    }

    public void RemoveMana(int rmMana)
    {
            mana = mana-rmMana;
            for(int i = 0; i < manaBlibs.Length; i++)
            {
                manaBlibs[i].SetActive(false);
            }

            for(int i = 0; i < mana; i++)
            {
                manaBlibs[i].SetActive(true);
            }

            Debug.Log("Casting Card total mana available: " + mana);

    }
}

