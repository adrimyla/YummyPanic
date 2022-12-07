using System.Collections;
using System.Collections.Generic;
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

        foreach(var item in items)
        {
            GameObject obj = Instantiate(SlotItem, ItemContainer);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemIcon.sprite = item.icon; 
        }
    }
}
