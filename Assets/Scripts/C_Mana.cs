using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Mana : MonoBehaviour
{

    public int mana;
    private int maxMana;
    public GameObject[] manaBlibs;
    private IA_Game _inputActionReference;

    // Start is called before the first frame update
    void Start()
    {
        _inputActionReference = new IA_Game();

         _inputActionReference.Enable();

        _inputActionReference.Player.DebugMana.performed += movelarm => { DebugMana(); };


        SetMana();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckMana()
    {
        maxMana++;
        if (maxMana >= 10)
        {
            maxMana = 10;
            Debug.Log("Max Mana reached");
        }
    }
    public void SetMana()
    {
        maxMana = mana;
        for(int i = 0; i < mana; i++)
        {
            manaBlibs[i].SetActive(true);
        } 
    }

    public void RemoveMana(int rmMana)
    {
        if(mana <= rmMana)
        {
            Debug.Log("Not enough mana to cast card");
        }
        else
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
            //TODO put spell fuction here
        }

    }
    public void DebugMana()
    {
        #if UNITY_EDITOR
        {
            RemoveMana(2);
        }
        #endif
    }
}
