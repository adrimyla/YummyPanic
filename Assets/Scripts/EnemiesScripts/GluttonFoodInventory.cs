using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonFoodInventory : InventoryManager
{
    [SerializeField] private int _limit = 1; // maximum amount of food a glutton can keep
    public bool isFull = false; // whenever a glutton has the maximum amount it can keep at once or not
    public bool isEmpty = true; // whenever a glutton hasn't any food in its "inventory" (or stomach ? who knows)

    /// <summary>
    /// Taking the food
    /// </summary>
    /// <param name="food"></param>
    public override void Add(Item item)
    {
        items.Add(item);
        //food.transform.position = gameObject.transform.position;
        //// Desactivate the food, to prevent it from showing in the scene. Destroying it would cause issues with the food list and lead to missing objects.
        //food.SetActive(false);
        CheckInventorySpace();
    }

    /// <summary>
    /// Remove all foods from the inventory. It'll be called when the glutton is back to its burrow
    /// </summary>
    public void FreeInventory()
    {
        //foreach(var food in items)
        //    Destroy(food.gameObject);
        items.Clear();
        CheckInventorySpace();
    }

    /// <summary>
    /// Check the amount of food the glutton is currently holding
    /// </summary>
    void CheckInventorySpace()
    {
        if (items.Count == _limit)
        {
            isFull = true;
            if (isEmpty)
                isEmpty = false;
        }
        else if (items.Count == 0)
        {
            isEmpty = true;
            if (isFull)
                isFull = false;
        }
        else
        {
            isEmpty = false;
            isFull = false;
        }
    }

    public override void StealItems(InventoryManager stolenInventory)
    {
        foreach(Item item in stolenInventory.items)
        {
            if (isFull)
                break;
            else
                Add(item);
        }
    }
}
