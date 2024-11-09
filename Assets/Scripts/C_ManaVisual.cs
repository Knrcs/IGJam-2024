using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ManaVisual : MonoBehaviour
{
    public GameObject[] allChildren;

    // Start is called before the first frame update
    void Start()
    {
        allChildren = new GameObject[transform.childCount];
        DoChildrenLookup();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoChildrenLookup()
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        // //Now destroy them
        // foreach (GameObject child in allChildren)
        // {
        //     DestroyImmediate(child.gameObject);
        // }

    }
}
