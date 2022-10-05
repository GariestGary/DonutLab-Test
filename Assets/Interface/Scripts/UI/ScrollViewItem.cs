using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    
    public UnityEvent<ScrollViewItem> OnClick;
    public GameObject Content { get; private set; }
    public string Name { get; private set; }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke(this);
    }

    public void Initialize(string name, Sprite icon, GameObject content)
    {
        Name = name;
        image.sprite = icon;
        Content = content;
    }
}
