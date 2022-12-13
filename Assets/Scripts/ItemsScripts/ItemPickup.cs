using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    void Pickup(InventoryManager inv)
    {
        //Add object to inventory
        inv.Add(item);

        //Update player score
        GameManager.Instance.UpdatePlayerScore(item.value);

        //Destroy object in scene
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
