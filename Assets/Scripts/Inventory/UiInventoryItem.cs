using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInventoryItem : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _quantityText;
    [SerializeField] private Image _borderImage;

    // public event Action<UiInventoryItem> OnItemClicked, OnRightMouseBtnClicked;
    
    private bool _empty = true;

    private void Awake()
    {
        ResetData();
    }

    public void ResetData()
    {
        _itemImage.gameObject.SetActive(false);
        _empty = true;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        _itemImage.gameObject.SetActive(true);
        _itemImage.sprite = sprite;
        _quantityText.text = quantity + "";
        _empty = false;
    }
}
