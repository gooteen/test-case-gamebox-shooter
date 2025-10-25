using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameItems _gameItems;
    [SerializeField] private Transform _autoWeaponSpawnPoint;
    [SerializeField] private Transform _singleShotWeaponSpawnPoint;
    [SerializeField] private SceneItem _itemCandidate;
    private PlayerController _player;

    public SceneItem ItemCandidate
    {
        get { return _itemCandidate; }
        set { _itemCandidate = value; }
    }

    private void Awake()
    {
        _itemCandidate = null;
        _player = GetComponent<PlayerController>();
        foreach (var item in _inventory.items)
        {
            item.itemId = -1;
            item.objectInGame = null;
            item.quantity = 0;
        }
    }

    void Start()
    {
        _inventory.currentSelectedItem = 0;
        GameEvents.RaiseItemSelected(_inventory.currentSelectedItem);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Seek(-1);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Seek(1);

        SeekByIndex();

        PickItem();
    }

    public void AddItem(int itemIndex, int quantity, int cellIndex)
    {
        InventoryItem currentItem = _inventory.items[_inventory.currentSelectedItem];

        for (int i = 0; i < _inventory.items.Length; i++)
        {
            if (_inventory.items[i].itemId == itemIndex)
            {
                _inventory.items[i].quantity += quantity;
                GameEvents.RaiseItemQuantityUpdated(i, _inventory.items[i].quantity);
                return;
            }
        }
        
        InventoryItem newItem;

        if (itemIndex == 0)
        {
            GameObject item = Instantiate(_gameItems.items[itemIndex].itemPrefab, _singleShotWeaponSpawnPoint);
            newItem = new InventoryItem(itemIndex, item, quantity);
            _inventory.items[cellIndex] = newItem;
            EquipWeapon();
        }
        else if (itemIndex == 1)
        {
            GameObject item = Instantiate(_gameItems.items[itemIndex].itemPrefab, _autoWeaponSpawnPoint);
            newItem = new InventoryItem(itemIndex, item, quantity);
            _inventory.items[cellIndex] = newItem;
            EquipWeapon();
        } else
        {
            newItem = new InventoryItem(itemIndex, quantity);
            _inventory.items[cellIndex] = newItem;
        }

        GameEvents.RaiseItemAdded(cellIndex, itemIndex, quantity);
        
    }

    private void PickItem()
    {
        int cellIndex = FindFirstEmptyInventoryCell();
        if (_itemCandidate != null && Input.GetKeyDown(KeyCode.E) && cellIndex != -1)
        {
            AddItem(_itemCandidate.ItemIndex, _itemCandidate.ItemQuantity, cellIndex);
            Destroy(_itemCandidate.gameObject);
            _itemCandidate = null;
            GameEvents.RaiseItemPickupUnavailable();
        }
    }

    private int FindFirstEmptyInventoryCell()
    {
        for (int i = 0; i < _inventory.items.Length; i++)
        {
            if (_inventory.items[i].itemId == -1)
            {
                return i;
            }
        }
        return -1;
    }

    private void SeekByIndex()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Alpha5) ||
            Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Alpha7) ||
            Input.GetKeyDown(KeyCode.Alpha8) ||
            Input.GetKeyDown(KeyCode.Alpha9))
        {
            UnequipWeapon();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _inventory.currentSelectedItem = 0;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _inventory.currentSelectedItem = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _inventory.currentSelectedItem = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _inventory.currentSelectedItem = 3;
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _inventory.currentSelectedItem = 4;
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                _inventory.currentSelectedItem = 5;
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                _inventory.currentSelectedItem = 6;
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                _inventory.currentSelectedItem = 7;
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                _inventory.currentSelectedItem = 8;
            }

            EquipWeapon();
        }
        GameEvents.RaiseItemSelected(_inventory.currentSelectedItem);
    }

    private void Seek(int step)
    {
        UnequipWeapon();

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

        EquipWeapon();

        GameEvents.RaiseItemSelected(_inventory.currentSelectedItem);
    }

    private void EquipWeapon()
    {
        InventoryItem currentItem = _inventory.items[_inventory.currentSelectedItem];

        if (currentItem.objectInGame != null && (currentItem.itemId == 0 || currentItem.itemId == 1))
        {
            currentItem.objectInGame.SetActive(true);
            Weapon w = currentItem.objectInGame.GetComponent<Weapon>();
            _player.EquippedWeapon = w;
        }
        
    }

    private void UnequipWeapon()
    {
        InventoryItem currentItem = _inventory.items[_inventory.currentSelectedItem];

        if (currentItem.objectInGame != null && (currentItem.itemId == 0 || currentItem.itemId == 1))
        {
            currentItem.objectInGame.SetActive(false);
            _player.EquippedWeapon = null;
        }
    }

}
