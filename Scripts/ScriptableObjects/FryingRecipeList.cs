using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu()]
public class FryingRecipeList : ScriptableObject
{
    public List<FryingRecipe> list;
    
    public bool TryGetFryingRecipe(KitchenObjectDef input, out FryingRecipe fryingRecipe) {
        foreach(FryingRecipe fryingrecipe in list) {
            if(fryingrecipe.input == input) {
                fryingRecipe = fryingrecipe;
                return true;
            }
        }
        fryingRecipe = null;
        return false;
    }
}

[Serializable]
public class FryingRecipe {
    public KitchenObjectDef input;
    public KitchenObjectDef output;
    public float fryingTime;
}