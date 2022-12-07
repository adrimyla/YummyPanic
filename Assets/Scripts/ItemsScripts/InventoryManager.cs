using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    public GameObject SlotItem;
    public Transform ItemContainer;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
        ListItems();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        ListItems();
    }

    public void ListItems()
    {

        //Clean content
        foreach(Transform item in ItemContainer)
        {
            Destroy(item.gameObject);
        }

        Dictionary<Item, int> currentItems = new Dictionary<Item, int>();

        //Browse items
        foreach (var item in items)
        {
            if (currentItems.ContainsKey(item))
            {
                currentItems[item] += 1;           
            }
            else
            {
                currentItems.Add(item, 1);                
            }
        
        }

        //Display items
        foreach(var item in currentItems)
        {
            GameObject obj = Instantiate(SlotItem, ItemContainer);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCount = obj.transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
            itemIcon.sprite = item.Key.icon;
            itemCount.text = item.Value.ToString();
        }
    }
}
