using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject selectedCounter;

    public void SelectCounter() {
        selectedCounter.SetActive(true);
    }
    public void CancelSelect() {
        selectedCounter.SetActive(false);
    }

    public virtual void Interact(Player player) {
        if (player.IsHaveKitchenObject()) {
            if (!IsHaveKitchenObject()) {
                TransferKitchenObject(player, this);
            }
            else {
                if (player.GetKitchenObject().TryGetComponent<Plate>(out Plate plate_1)) {
                    if (plate_1.AddKitchenObjectDef(GetKitchenObject().GetKitchenObjectDef())){
                        DestroyKitchenObject();
                    }
                    
                }
                else {
                    if(GetKitchenObject().TryGetComponent<Plate>(out Plate plate_2)) {
                        if (plate_2.AddKitchenObjectDef(player.GetKitchenObject().GetKitchenObjectDef())) {
                            player.DestroyKitchenObject();
                        }
                    }
                }
            }
        }
        else {
            if (IsHaveKitchenObject()) {
                TransferKitchenObject(this, player);
            }
        }
        /*if(IsHaveKitchenObject() && !player.IsHaveKitchenObject()) { 
            TransferKitchenObject(this, player);
        }
        else if(!IsHaveKitchenObject() && player.IsHaveKitchenObject()) {
            TransferKitchenObject(player, this);
        }
        else {
            Debug.Log("Nothing to do");
        }*/

    }

    public virtual void InteractOperate(Player player) {
        return;
    }
}
