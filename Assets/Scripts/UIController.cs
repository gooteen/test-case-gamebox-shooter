using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _inventoryCellContainer;
    [SerializeField] private GameObject _inventoryCellPrefab;
    [SerializeField] private GameObject _pickupTip;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameItems _gameItems;
    private List<InventoryPanelCell> _cells;

    private void OnEnable()
    {
        GameEvents.OnItemAdded += UpdateCellData;
        GameEvents.OnItemSelected += UpdateCursorPosition;
        GameEvents.OnItemPickupAvailable += ShowPickupTip;
        GameEvents.OnItemPickupUnavailable += HidePickupTip;
    }

    private void OnDisable()
    {
        GameEvents.OnItemAdded -= UpdateCellData;
        GameEvents.OnItemSelected -= UpdateCursorPosition;
        GameEvents.OnItemPickupAvailable += ShowPickupTip;
        GameEvents.OnItemPickupUnavailable += HidePickupTip;

    }

    void Start()
    {
        _cells = new List<InventoryPanelCell>();

        for (int i = 0; i < _inventory.items.Length; i++)
        {
            GameObject cell = Instantiate(_inventoryCellPrefab, _inventoryCellContainer);
            InventoryPanelCell cellData = cell.GetComponent<InventoryPanelCell>();
            _cells.Add(cellData);
        }
    }
    private void UpdateCellQuantity(int cellIndex, int quantity)
    {
        _cells[cellIndex].QuantityValue.text = quantity.ToString();
    }

    private void UpdateCellData(int cellIndex, int itemIndex, int quantity)
    {
        _cells[cellIndex].QuantityValue.enabled = true;
        _cells[cellIndex].QuantityValue.text = quantity.ToString();
        _cells[cellIndex].Image.enabled = true;
        _cells[cellIndex].Image.sprite = _gameItems.items[itemIndex].icon;
    }

    private void ShowPickupTip()
    {
        _pickupTip.SetActive(true);
    }

    private void HidePickupTip()
    {
        _pickupTip.SetActive(false);
    }

    private void UpdateCursorPosition(int index)
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            if (i == index)
            {
                _cells[i].Cursor.SetActive(true);
            } else
            {
                _cells[i].Cursor.SetActive(false);
            }
        }
    }
}
