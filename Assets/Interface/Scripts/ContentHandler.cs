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
            content.items.ForEach(i =>
            {
                i.InstantiatedGameObject = i.Prefab ? Instantiate(i.Prefab, Vector3.zero, Quaternion.identity, itemViewRoot) : null;
                i.InstantiatedGameObject?.SetActive(false);
            });
            
            //Add scroll views with content and corresponding buttons
            TabContent tabContent = tabContainer.AddContent(content);
            tabButtonsContainer.AddButton(content.ContentIcon, _ => tabContainer.Show(tabContent));

            //select first item if there is no one selected
            if (content.CurrentSelected.InstantiatedGameObject == null && content.items.Count != 0)
            {
                content.CurrentSelected = content.items[0];
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
        
        currentViewHandler.Show(scrollButton);
    }

    private void Start()
    {
        tabButtonsContainer.Click(0);
        tabContainer.UpdateSelected();
        tabContainer.ClickItem(tabContainer.CurrentShownContent.Group.CurrentSelected);
    }

    public void SaveCurrentSelectedItem()
    {
        Item currentItem = tabContainer.CurrentShownContent.Group.CurrentSelected;
        Item newItem = tabContainer.CurrentClickedButton.AssociatedItem;
        
        if (currentItem != newItem)
        {
            tabContainer.CurrentShownContent.Group.CurrentSelected = newItem;
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


