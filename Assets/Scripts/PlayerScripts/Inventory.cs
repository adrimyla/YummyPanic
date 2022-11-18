using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int limit = 3; // quantit� maximale d'ingr�dients que le joueur peut poss�der
    List<GameObject> foods; // Ingr�dients poss�d� par le joueur

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Food") && foods.Count < limit)
        {
            foods.Add(other.gameObject);
        }
    }
}
