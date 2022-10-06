using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Interface/Content Data")]
public class ContentData : ScriptableObject
{
    public List<Group> contents;
}

[System.Serializable]
public class Group
{
    public string Name;
    public Sprite ContentIcon;
    public int OrderInHierarchy;
    public List<Item> items;
    [HideInInspector] public string CurrentSelected;

    public Item GetCurrentSelected()
    {
        return items.FirstOrDefault(i => i.Name == CurrentSelected);
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public Sprite Sprite;
    public GameObject Prefab;
    [HideInInspector] public GameObject InstantiatedGameObject;
}