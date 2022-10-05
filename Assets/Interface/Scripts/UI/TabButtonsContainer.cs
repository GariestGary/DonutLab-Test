using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TabButtonsContainer : MonoBehaviour
{
    [SerializeField] private GameObject tabButtonPrefab;
    [SerializeField] private Transform buttonsRoot;

    private List<TabButton> buttons = new List<TabButton>();

    private TabButton currentSelectedButton;

    public TabButton AddButton(Sprite icon, UnityAction<TabButton> callback)
    {
        TabButton button = Instantiate(tabButtonPrefab, Vector3.zero, Quaternion.identity, buttonsRoot).GetComponent<TabButton>();
        button.onClickEvent.AddListener(callback);
        button.onClickEvent.AddListener(Select);
        button.SetIcon(icon);
        buttons.Add(button);
        return button;
    }

    public void Select(TabButton button)
    {
        if (button == null)
        {
            return;
        }
        
        currentSelectedButton?.Deselect();
        button.Select();

        currentSelectedButton = button;
    }

    public void Click(int index)
    {
        GetButtonByIndex(index).Click();
    }

    public void Select(int index)
    {
        TabButton selectedButton = GetButtonByIndex(index);
        Select(selectedButton);
    }

    private TabButton GetButtonByIndex(int index)
    {
        if (index < 0 || index >= buttons.Count)
        {
            return null;
        }

        return buttons[index];
    }
    
    
}
