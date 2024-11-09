using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using  UnityEngine.UI;

public class C_Bottleneck : MonoBehaviour
{
    public TMP_Text damageModifyerText;
    public int extraDamage;
    public GameObject[] cardStack;
    public Image bottleNeckFill;

    // Start is called before the first frame update
    void Start()
    {
        bottleNeckFill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
