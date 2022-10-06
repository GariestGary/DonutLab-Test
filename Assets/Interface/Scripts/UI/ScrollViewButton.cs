using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScrollViewButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    
    public UnityEvent<ScrollViewButton> ClickedEvent;
    public TabContent GroupContent { get; private set; }
    
    public Item AssociatedItem { get; private set; }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    public void Click()
    {
        ClickedEvent.Invoke(this);
    }

    public void Initialize(Sprite icon, Item item, TabContent group)
    {
        image.sprite = icon;
        GroupContent = group;
        AssociatedItem = item;
    }
}
