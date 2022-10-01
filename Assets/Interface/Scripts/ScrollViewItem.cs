using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    
    public event Action<ScrollViewItem> OnClick;

    public object Content { get; private set; }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke(this);
    }

    public void Initialize(Sprite icon, object content)
    {
        image.sprite = icon;
        Content = content;
    }
}
