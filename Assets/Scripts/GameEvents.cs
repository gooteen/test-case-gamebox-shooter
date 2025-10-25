using System;
using UnityEngine;

public static class GameEvents
{
    // Инвентарь изменился (добавлен/удалён предмет, изменилось количество)
    public static event Action<int, int, int> OnItemAdded;

    public static event Action<int, int> OnItemQuantityChanged;

    // Активный предмет изменился
    public static event Action<int> OnItemSelected;

    // Игрок подошёл к предмету на сцене, который можно взять
    public static event Action OnItemPickupAvailable;

    // Игрок отошёл от предмета
    public static event Action OnItemPickupUnavailable;

    public static void RaiseItemAdded(int cellIndex, int itemIndex, int quantity) => OnItemAdded?.Invoke(cellIndex, itemIndex, quantity);
    public static void RaiseItemQuantityUpdated(int cellIndex, int quantity) => OnItemQuantityChanged?.Invoke(cellIndex, quantity);
    public static void RaiseItemSelected(int index) => OnItemSelected?.Invoke(index);
    public static void RaiseItemPickupAvailable() => OnItemPickupAvailable?.Invoke();
    public static void RaiseItemPickupUnavailable() => OnItemPickupUnavailable?.Invoke();
}
