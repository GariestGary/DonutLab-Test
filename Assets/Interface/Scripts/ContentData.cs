using System.Collections;
using System.Collections.Generic;
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
    public List<Item> items;
    [HideInInspector] public Item CurrentSelected;
}

[System.Serializable]
public class Item
{
    public string Name;
    public Sprite Sprite;
    public GameObject Prefab;
    [HideInInspector] public GameObject InstantiatedGameObject;
}