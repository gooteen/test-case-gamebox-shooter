using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItem : MonoBehaviour
{
    [SerializeField] private int _itemIndex;
    [SerializeField] private int _itemQuantity;
    
    public int ItemIndex
    {
        get { return _itemIndex; }
        set { _itemIndex = value; }
    }

    public int ItemQuantity
    {
        get { return _itemQuantity; }
        set { _itemQuantity = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        InventoryController controller = other.GetComponent<InventoryController>();
        controller.ItemCandidate = this;
        GameEvents.RaiseItemPickupAvailable();
    }

    private void OnTriggerExit(Collider other)
    {
        InventoryController controller = other.GetComponent<InventoryController>();
        controller.ItemCandidate = null;
        GameEvents.RaiseItemPickupUnavailable();
    }
}
