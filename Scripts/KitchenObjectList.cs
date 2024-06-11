using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectList : ScriptableObject
{
    public List<KitchenGameObject> kitchenObjectList = new List<KitchenGameObject>();

    public bool TryGetKitchenGameObject(KitchenObjectDef kitchenObjectDef, out GameObject gameObject) {
        foreach(KitchenGameObject kitchenObjectPair in kitchenObjectList) {
            if(kitchenObjectPair.kitchenObjectDef == kitchenObjectDef) {
                gameObject = kitchenObjectPair.gameObject;
                return true;
            }
        }
        gameObject = null;
        return false;
    }
}

[Serializable]
public class KitchenGameObject {
    public KitchenObjectDef kitchenObjectDef;
    public GameObject gameObject;
}