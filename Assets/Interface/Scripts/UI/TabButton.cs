using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite basicSprite;
    [SerializeField] private string tabName;

    public string TabName => tabName;
    public UnityEvent<TabButton> onClickEvent;
    
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        Deselect();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }
    
    public void Click()
    {
        onClickEvent.Invoke(this);
    }

    public void Select()
    {
        image.sprite = selectedSprite;
    }

    public void Deselect()
    {
        image.sprite = basicSprite;
    }
}
