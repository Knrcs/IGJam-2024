using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class C_PlayCard : MonoBehaviour, IPointerDownHandler
{
    public C_Mana manaFunction;
    public C_Health healthFunction;
    public C_CardInfo cardInfoFunction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Card Pressed" + eventData.pointerId);
        Destroy(gameObject);
    }
}
