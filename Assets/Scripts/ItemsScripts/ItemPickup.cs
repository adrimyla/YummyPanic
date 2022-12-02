using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    void Pickup(InventoryManager inv)
    {
        inv.Add(item);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            InventoryManager inv = other.GetComponentInParent<InventoryManager>();
            Pickup(inv);
        }
        
    }

}
