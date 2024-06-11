using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]private KitchenObjectDef kitchenObjectDef;

    public KitchenObjectDef GetKitchenObjectDef() {
        return kitchenObjectDef;
    }
    public string GetKitchenObjectStringName() {
        return kitchenObjectDef.objectName;
    }
}
