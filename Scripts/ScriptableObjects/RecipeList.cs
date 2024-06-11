using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeList : ScriptableObject {
    public List<Recipe> recipeList;



}

[Serializable]
public class Recipe {
    public string recipeName;
    public List<KitchenObjectDef> kitchenObjectDefs;
}


