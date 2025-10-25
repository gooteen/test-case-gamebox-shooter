using System;
using UnityEngine;

public static class GameEvents
{
    // ��������� ��������� (��������/����� �������, ���������� ����������)
    public static event Action<int, int, int> OnItemAdded;

    public static event Action<int, int> OnItemQuantityChanged;

    // �������� ������� ���������
    public static event Action<int> OnItemSelected;

    // ����� ������� � �������� �� �����, ������� ����� �����
    public static event Action OnItemPickupAvailable;

    // ����� ������ �� ��������
    public static event Action OnItemPickupUnavailable;

    public static void RaiseItemAdded(int cellIndex, int itemIndex, int quantity) => OnItemAdded?.Invoke(cellIndex, itemIndex, quantity);
    public static void RaiseItemQuantityUpdated(int cellIndex, int quantity) => OnItemQuantityChanged?.Invoke(cellIndex, quantity);
    public static void RaiseItemSelected(int index) => OnItemSelected?.Invoke(index);
    public static void RaiseItemPickupAvailable() => OnItemPickupAvailable?.Invoke();
    public static void RaiseItemPickupUnavailable() => OnItemPickupUnavailable?.Invoke();
}
