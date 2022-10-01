using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabButtonsContainer : MonoBehaviour
{
    [SerializeField] private TabContainer tabContainer;
    
    private List<TabButton> buttons = new List<TabButton>();
    

    private void Awake()
    {
        foreach (var button in GetComponentsInChildren<TabButton>())
        {
            buttons.Add(button);
            button.onClickEvent.AddListener(ButtonClickEventHandler);
        }

        if (buttons.Count > 0)
        {
            buttons[0].Click();
        }
    }

    private void ButtonClickEventHandler(TabButton button)
    {
        button.Select();
        buttons.ForEach(b => {if(b != button) b.Deselect();});
        tabContainer.Show(button.TabName);
    }
}
