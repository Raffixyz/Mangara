using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private UiInventoryItem item;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Mouse.current.position.ReadValue(),
           null,
            out var position);
        ((RectTransform)transform).anchoredPosition = position;
    }

    public void Toogle(bool val)
    {
        item.gameObject.SetActive(val);
    }
    
}
