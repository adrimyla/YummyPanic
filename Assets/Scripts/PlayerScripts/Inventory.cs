using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int limit = 3; // quantité maximale d'ingrédients que le joueur peut posséder
    List<GameObject> foods; // Ingrédients possédé par le joueur

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Food") && foods.Count < limit)
        {
            foods.Add(other.gameObject);
        }
    }
}
