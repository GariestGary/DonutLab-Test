using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHandler : MonoBehaviour
{
    [SerializeField] private List<Content> contents;
    [SerializeField] private TabContainer tabContainer;
    [SerializeField] private TabButtonsContainer tabButtonsContainer;
    [SerializeField] private Transform itemViewRoot;

    private void Awake()
    {
        foreach (var content in contents)
        {
            content.items.ForEach(i =>
            {
                i.GameObject = i.GameObject ? Instantiate(i.GameObject, Vector3.zero, Quaternion.identity, itemViewRoot) : null;
                i.GameObject?.SetActive(false);
            });
            TabContent tabContent = tabContainer.AddContent(content);
            tabButtonsContainer.AddButton(content.ContentIcon, _ => tabContainer.Show(tabContent));
        }
    }

    private void Start()
    {
        tabButtonsContainer.Click(0);

    }
}

[System.Serializable]
public class Content
{
    public string Name;
    public Sprite ContentIcon;
    public List<Item> items;
}

[System.Serializable]
public class Item
{
    public string Name;
    public Sprite Sprite;
    public GameObject GameObject;
}
