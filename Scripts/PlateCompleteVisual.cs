using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    
    [Serializable]
    private class PlateKitchenObject {
        public KitchenObjectDef kitchenObjectDef;
        public GameObject gameObject;
    }
    [SerializeField] private List<PlateKitchenObject> plateKitchenObjects;
    public void show(KitchenObjectDef kitchenObjectDef) {
        foreach(PlateKitchenObject pair in plateKitchenObjects) {
            if(pair.kitchenObjectDef == kitchenObjectDef) {
                pair.gameObject.SetActive(true);
            }
        }
        
    }
}
