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
    }

    public int ItemQuantity
    {
        get { return _itemQuantity; }
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
