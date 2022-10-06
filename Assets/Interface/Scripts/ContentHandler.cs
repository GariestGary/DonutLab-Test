using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ContentHandler : MonoBehaviour
{
    [SerializeField] private List<ViewHandlerData> viewHandlers;
    [SerializeField] private ContentData data;
    [SerializeField] private TabContainer tabContainer;
    [SerializeField] private TabButtonsContainer tabButtonsContainer;
    [SerializeField] private Transform itemViewRoot;

    private IItemView currentViewHandler;

    private void Awake()
    {
        foreach (var content in data.contents)
        {
            //Instantiating prefabs
            GameObject groupRoot = new GameObject(content.Name + " Group");
            groupRoot.transform.SetParent(itemViewRoot);
            groupRoot.transform.SetSiblingIndex(content.OrderInHierarchy);
            content.items.ForEach(i =>
            {
                i.InstantiatedGameObject = i.Prefab ? Instantiate(i.Prefab, Vector3.zero, Quaternion.identity, groupRoot.transform) : null;
                i.InstantiatedGameObject?.SetActive(false);
            });
            
            //Add scroll views with content and corresponding buttons
            TabContent tabContent = tabContainer.AddContent(content);
            tabButtonsContainer.AddButton(content.ContentIcon, _ => tabContainer.Show(tabContent));

            //select first item if there is no one selected or it doesn't exist
            Debug.Log(content.CurrentSelected);
            
            if ((string.IsNullOrEmpty(content.CurrentSelected) || content.items.All(i => i.Name != content.CurrentSelected)) && content.items.Count != 0)
            {
                content.CurrentSelected = content.items[0].Name;
            }

        }
        
        tabContainer.ItemClickedEvent.AddListener(ItemSelectedHandler);
        tabContainer.ShownContentChanged.AddListener(UpdateHandler);
    }

    private void UpdateHandler(Group group)
    {
        ViewHandlerData viewHandlerData = viewHandlers.FirstOrDefault(h => h.GroupName == group.Name);

        if (viewHandlerData == null)
        {
            Debug.LogWarning($"Handler associated with {group.Name} group doesn't exist");
            return;
        }

        currentViewHandler = viewHandlerData.ViewHandler.GetComponentInChildren<IItemView>();
    }

    private void ItemSelectedHandler(ScrollViewButton scrollButton)
    {
        if (scrollButton == null)
        {
            return;
        }

        if (currentViewHandler.CurrentShownItem != scrollButton)
        {
            currentViewHandler.Show(scrollButton);
        }
    }

    private void Start()
    {
        tabContainer.UpdateSelected();
        foreach (var content in data.contents)
        {
            tabContainer.ClickItem(content.GetCurrentSelected());
        }
        tabButtonsContainer.Click(0);
    }

    public void SaveCurrentSelectedItem()
    {
        Item currentItem = tabContainer.CurrentShownContent.Group.GetCurrentSelected();
        Item newItem = tabContainer.CurrentClickedButton.AssociatedItem;
        
        if (currentItem != newItem)
        {
            tabContainer.CurrentShownContent.Group.CurrentSelected = newItem.Name;
            tabContainer.UpdateSelected();
        }
    }
}

[System.Serializable]
public class ViewHandlerData
{
    public string GroupName;
    public GameObject ViewHandler;
}


