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
    public int quantity;

    public InventoryItem(int id, GameObject o, int q)
    {
        itemId = id;
        objectInGame = o;
        quantity = q;
    }

    public InventoryItem(int id, int q)
    {
        itemId = id;
        objectInGame = null;
        quantity = q;
    }
}


