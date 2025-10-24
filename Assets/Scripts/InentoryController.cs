using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InentoryController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameItems _gameItems;
    [SerializeField] private Transform _autoWeaponSpawnPoint;
    [SerializeField] private Transform _singleShotWeaponSpawnPoint;
    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
        foreach (var item in _inventory.items)
        {
            item.itemId = -1;
            item.objectInGame = null;
        }
    }
    void Start()
    {
        _inventory.currentSelectedItem = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            AddItem(1);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Seek(-1);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Seek(1);
    }

    public void AddItem(int itemIndex)
    {
        InventoryItem currentItem = _inventory.items[_inventory.currentSelectedItem];
        if (currentItem != null)
        {
            if (itemIndex == 1)
            {
                GameObject item = Instantiate(_gameItems.items[itemIndex].itemPrefab, _autoWeaponSpawnPoint);
                currentItem = new InventoryItem(itemIndex, item);
                _inventory.items[_inventory.currentSelectedItem] = currentItem;
                _player.EquipWeapon(item);
            }
        }
    }

    private void SeekByIndex(int index)
    {

        Input.GetKeyDown(KeyCode.Alpha1);
    }

    private void Seek(int step)
    {
        InventoryItem currentItem = _inventory.items[_inventory.currentSelectedItem];

        if (currentItem.objectInGame != null)
        {
            _player.UnequipWeapon(currentItem.objectInGame);
        }

        int newIndex = _inventory.currentSelectedItem + step;
        if (newIndex == _inventory.items.Length)

        {
            _inventory.currentSelectedItem = 0;
        } else if (newIndex == -1)
        {
            _inventory.currentSelectedItem = _inventory.items.Length - 1;
        } else
        {
            _inventory.currentSelectedItem = newIndex;
        }

        currentItem = _inventory.items[_inventory.currentSelectedItem];

        if (currentItem.objectInGame != null)
        {
            _player.EquipWeapon(currentItem.objectInGame);
        }
    }
    
}
