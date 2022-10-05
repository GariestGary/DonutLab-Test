using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabContainer : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewItemPrefab;
    [SerializeField] private Transform contentRoot;
    
    private List<TabContent> contents = new List<TabContent>();
    private TabContent currentShownContent;

    public TabContent AddContent(Content content)
    {
        TabContent tabContent = new TabContent();
        tabContent.Icon = content.ContentIcon;
        tabContent.Name = content.Name;
        foreach (var item in content.items)
        {
            ScrollViewItem scrollItem = Instantiate(scrollViewItemPrefab, Vector3.zero, Quaternion.identity, contentRoot).GetComponent<ScrollViewItem>();
            scrollItem.gameObject.SetActive(false);
            scrollItem.Initialize(item.Name, item.Sprite, item.GameObject);
            tabContent.Items.Add(scrollItem);
        }
        contents.Add(tabContent);
        return tabContent;
    }

    public void Show(TabContent content)
    {
        if (content == null)
        {
            return;
        }
        
        currentShownContent?.Items?.ForEach(c => c.gameObject.SetActive(false));
        content.Items?.ForEach(c => c.gameObject.SetActive(true));
        currentShownContent = content;
    }
}

[System.Serializable]
public class TabContent
{
    public string Name;
    public Sprite Icon;
    public List<ScrollViewItem> Items = new List<ScrollViewItem>();
}


