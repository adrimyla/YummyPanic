using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public static int RecipeCount = 1;
    public int ID { get; set; }
    private List<GameObject> ingredients;

    public Recipe(List<GameObject> _ingredients)
    {
        ID = RecipeCount;
        ingredients = _ingredients;
        RecipeCount++;
    }
    public void DebugPrintRecipe()
    {
        string ingredientList = "";
        foreach(var ingredient in ingredients)
        {
            ingredientList+= ingredient.ToString() + ",";
        }
        Debug.Log("RECIPE N° " + ID + "\n" + "INGREDIENTS :" + ingredientList + "\n");
    }
}
