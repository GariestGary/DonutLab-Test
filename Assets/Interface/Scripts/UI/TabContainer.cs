using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TabContainer : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewItemPrefab;
    [SerializeField] private Transform contentRoot;
    [SerializeField] private GameObject selectionFrame;
    [SerializeField] private GameObject selectedSign;

    public UnityEvent<ScrollViewButton> ItemClickedEvent;
    public UnityEvent<Group> ShownContentChanged;
    public UnityEvent<string> contentNameChanged;

    private List<TabContent> contents = new List<TabContent>();
    public TabContent CurrentShownContent { get; private set; }
    public ScrollViewButton CurrentClickedButton { get; private set; }

    public TabContent AddContent(Group group)
    {
        TabContent tabContent = new TabContent();

        foreach (var item in group.items)
        {
            ScrollViewButton scrollButton = Instantiate(scrollViewItemPrefab, Vector3.zero, Quaternion.identity, contentRoot).GetComponent<ScrollViewButton>();
            scrollButton.gameObject.SetActive(false);
            scrollButton.Initialize(item.Sprite, item, tabContent);
            scrollButton.ClickedEvent.AddListener(HandleItemClick);
            tabContent.Items.Add(scrollButton);
        }

        tabContent.Group = group;
        contents.Add(tabContent);
        return tabContent;
    }

    public void ClickItem(Item item)
    {
        foreach (var content in contents)
        {
            ScrollViewButton button = content.Items.FirstOrDefault(b => b.AssociatedItem == item);
            
            if (button != null)
            {
                if (content != CurrentShownContent)
                {
                    Show(content);
                }
                
                button.Click();
                break;
            }
        }
    }

    private void HandleItemClick(ScrollViewButton scrollButton)
    {
        if (CurrentClickedButton == scrollButton)
        {
            return;
        }
        
        
        ItemClickedEvent.Invoke(scrollButton);
        
        if (scrollButton == null)
        {
            return;
        }
        
        selectionFrame.SetActive(true);
        var itemTransform = scrollButton.transform;
        selectionFrame.transform.position = itemTransform.position;
        selectionFrame.transform.SetParent(itemTransform);
        CurrentClickedButton = scrollButton;
        CurrentShownContent.ClickedButton = scrollButton;
    }

    public void Show(TabContent content)
    {
        if (content == null)
        {
            return;
        }
        
        CurrentShownContent?.Items?.ForEach(c => c.gameObject.SetActive(false));
        content.Items?.ForEach(c => c.gameObject.SetActive(true));
        CurrentShownContent = content; 
        contentNameChanged.Invoke(CurrentShownContent.Group.Name);
        ShownContentChanged.Invoke(CurrentShownContent.Group);
        UpdateSelected();
        if (content.ClickedButton == null)
        {
            ClickItem(content.Group.GetCurrentSelected());
        }
        else
        {
            content.ClickedButton.Click();
        }
    }

    public void UpdateSelected()
    {
        ScrollViewButton scrollButton = CurrentShownContent.Items.FirstOrDefault(i => i.AssociatedItem == CurrentShownContent.Group.GetCurrentSelected());

        if (scrollButton == null)
        {
            selectedSign.SetActive(false);
        }
        else
        {
            selectedSign.SetActive(true);
            selectedSign.transform.position = scrollButton.transform.position;
            selectedSign.transform.SetParent(scrollButton.transform);
        }
    }
}

[System.Serializable]
public class TabContent
{
    public Group Group;
    public ScrollViewButton ClickedButton;
    public List<ScrollViewButton> Items = new List<ScrollViewButton>();
}


