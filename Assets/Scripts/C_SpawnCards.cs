using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class C_SpawnCards : MonoBehaviour
{
    public Transform playerHand;
    public GameObject prefab;
    public Cards[] cards;
    public int selCard;
    // Start is called before the first frame update
    void Start()
    {
        // SelectCard(0);
    }



    public void Spawn(int cardI)
    {
        GameObject kiddo = Instantiate(prefab);
        kiddo.transform.SetParent(playerHand);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
