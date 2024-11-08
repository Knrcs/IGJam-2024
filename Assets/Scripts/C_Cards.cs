using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Playcard", menuName = "ScriptableObjects/Create new playcard")]
public class Cards : ScriptableObject
{
    public string cardName;
    public string skill;
    public string description;
    public Sprite cardImage;
    public int manaCost;

}