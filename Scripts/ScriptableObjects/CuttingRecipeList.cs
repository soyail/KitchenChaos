using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CuttingRecipe
{
    public KitchenObjectDef input;
    public KitchenObjectDef output;
    public int cuttingCountMax;
}

[CreateAssetMenu()]
public class CuttingRecipeList : ScriptableObject {
    public List<CuttingRecipe> list;

    public KitchenObjectDef Getoutput(KitchenObjectDef input) {
        foreach(CuttingRecipe recipe in list) {
            if(recipe.input == input) {
                return recipe.output;
            }
        }
        return null;
    }

    public bool TryGetCuttingRecipe(KitchenObjectDef input, out CuttingRecipe cuttingRecipe) {
        foreach (CuttingRecipe recipe in list) {
            if (recipe.input == input) {
                cuttingRecipe = recipe;
                return true;
            }
        }
        cuttingRecipe = null;
        return false;
    }
}