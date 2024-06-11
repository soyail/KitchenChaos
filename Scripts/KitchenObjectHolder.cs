using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    
    private KitchenObject kitchenObject;
    // Start is called before the first frame update
    private void addKitchenObject(KitchenObject kitchenObject) {
        kitchenObject.gameObject.transform.SetParent(holdPoint);
        kitchenObject.gameObject.transform.localPosition = Vector3.zero;
        this.kitchenObject = kitchenObject;
    }
    private void ClearKitchenObject() {
        this.kitchenObject = null;
    }

    public Transform GetHoldPoint() {
        return holdPoint;
    }

    public KitchenObject GetKitchenObject() {  
        return kitchenObject; 
    }

    public bool IsHaveKitchenObject() {
        return kitchenObject != null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        kitchenObject.gameObject.transform.localPosition = Vector3.zero;
    }
    public void TransferKitchenObject(KitchenObjectHolder source, KitchenObjectHolder target) {
        if (source.kitchenObject && target.kitchenObject == null) {
            target.addKitchenObject(source.kitchenObject);
            source.ClearKitchenObject();
        }
    }
    public void DestroyKitchenObject() {
        Destroy(kitchenObject.gameObject);
        ClearKitchenObject();
    }
    public void CreateKitchenObject(GameObject KitchenObjectPrefab) {
        KitchenObject kitchenObject = GameObject.Instantiate(KitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }

    public bool CanTakeKitchenObject() {
        if(!GetKitchenObject() || GetKitchenObject().TryGetComponent<Plate>(out Plate plate)) {
            return true;
        }
        return false;
    }


}
