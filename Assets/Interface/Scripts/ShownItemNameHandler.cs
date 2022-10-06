using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShownItemNameHandler : MonoBehaviour
{
    [SerializeField] private TabContainer tabContainer;
    [SerializeField] private TextMeshProUGUI itemNameText;

    private void Awake()
    {
        tabContainer.ItemClickedEvent.AddListener(UpdateText);
    }

    private void UpdateText(ScrollViewButton button)
    {
        itemNameText.text = button.AssociatedItem.Name;
    }
}
