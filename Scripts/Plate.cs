using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Plate : KitchenObject
{

    [SerializeField] private List<KitchenObjectDef> kitchenobjects = new List<KitchenObjectDef>();
    [SerializeField] private PlateCompleteVisual plateCompleteVisual;
    public List<KitchenObjectDef> curKitchenObjects = new List<KitchenObjectDef>();

    public bool AddKitchenObjectDef(KitchenObjectDef kitchenObjectDef) {
        if (!curKitchenObjects.Contains(kitchenObjectDef) && kitchenobjects.Contains(kitchenObjectDef)) {
            plateCompleteVisual.show(kitchenObjectDef);
            curKitchenObjects.Add(kitchenObjectDef);
            return true;
        }
        return false;
    }

    public bool IsOrder() {
        return false;
    }
}
