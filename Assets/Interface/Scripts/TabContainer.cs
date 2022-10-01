using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabContainer : MonoBehaviour
{
    [SerializeField] private GameObject content;
    
    [SerializeField] private List<ContentList> lists = new List<ContentList>();

    public void Show(string contentName)
    {
        if (!lists.Any(x => x.Name == contentName))
        {
            return;
        }
        
        lists.ForEach(l =>
        {
            if (l.Name == contentName)
            {
                l.items.ForEach(i => i.gameObject.SetActive(true));
            }
            else
            {
                l.items.ForEach(i => i.gameObject.SetActive(false));
            }
        });
    }
    
    [System.Serializable]
    private class ContentList
    {
        public string Name;
        public List<ScrollViewItem> items;
    }
}
