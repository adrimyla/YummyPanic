using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    void Pickup(InventoryManager inv)
    {
        Debug.Log("[PLAYER] : picking up " + item.name);
        //Add object to inventory
        inv.Add(item);

        //Update player score
        GameManager.Instance.UpdatePlayerScore(item.value);

        //Destroy object in scene
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && item.type == itemType.FOOD)
        {
            InventoryManager inv = other.GetComponentInParent<InventoryManager>();
            Pickup(inv);
        }

        if(other.gameObject.tag == "Player" && item.type == itemType.BONUS)
        {
            if(item.name == "Clock")
            {
                Debug.Log("BONUS TIME !");
                GameManager.Instance.NotifyTimeBonus(item.value);
                Destroy(gameObject);
            }
        }
        
    }

}
