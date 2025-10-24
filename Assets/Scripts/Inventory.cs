using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]

public class Inventory : ScriptableObject
{
    public InventoryItem[] items;
    public int currentSelectedItem;
}

[System.Serializable]
public class InventoryItem
{
    public int itemId;
    public GameObject objectInGame;

    public InventoryItem(int id, GameObject o)
    {
        itemId = id;
        objectInGame = o;
    }
}


