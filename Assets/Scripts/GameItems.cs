using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameItems", menuName = "GameItems")]

public class GameItems : ScriptableObject
{
    public List<Item> items;   
}

[System.Serializable]
public class Item
{
    [Header("Base Item Settings")]
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;

    [TextArea]
    public string description;
}

