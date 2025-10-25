using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelCell : MonoBehaviour
{
    public int itemIndex;
    [SerializeField] private GameObject _cellImage;
    [SerializeField] private GameObject _cellQuantity;
    [SerializeField] private GameObject _cursor;

    [SerializeField] private TMP_Text _textValue;
    [SerializeField] private Image _imageValue;

    public GameObject Cursor
    {
        get { return _cursor; }
    }

    public TMP_Text QuantityValue
    {
        get { return _textValue; }
        set { _textValue = value; }
    }

    public Image Image
    {
        get { return _imageValue; }
    }

    private void Awake()
    {
        _textValue = _cellQuantity.GetComponent<TMP_Text>();
        _imageValue = _cellImage.GetComponent<Image>();
    }
}
