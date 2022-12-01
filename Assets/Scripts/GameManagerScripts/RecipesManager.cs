using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    private List<GameObject> foodList;
    public Recipe currentRecipe;
    public List<Recipe> OldRecipes;
    public int minIngredientPerRecipe;
    public int maxIngredientPerRecipe;

    public RecipesManager(List<GameObject> _food, int min, int max)
    {
        this.foodList = _food;
        this.minIngredientPerRecipe = min;
        this.maxIngredientPerRecipe = max;
    }

    public Recipe NewRandomRecipe()
    {
        int ingredientCount = Random.Range(minIngredientPerRecipe, maxIngredientPerRecipe);
        List<GameObject> ingredientList = new List<GameObject>(ingredientCount);

        for (int i = 0; i < ingredientCount; i++)
        {
            ingredientList.Add(foodList[Random.Range(0, foodList.Count)]);
        }
        return new Recipe(ingredientList);
    }


}
