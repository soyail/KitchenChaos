using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeList cuttingRecipeList;
    [SerializeField] private CuttingCounterVisual cuttingCounterVisual;
    [SerializeField] private KitchenObjectList kitchenObjectList;
    public override void Interact(Player player) {
        if (player.IsHaveKitchenObject()) {
            if (!IsHaveKitchenObject()) {
                TransferKitchenObject(player, this);
            }
            else {

            }
        }
        else {
            if(IsHaveKitchenObject()) {
                TransferKitchenObject(this, player);
            }
        }
        
    }

    public override void InteractOperate(Player player) {
        if (IsHaveKitchenObject()) {
            KitchenObjectDef output = cuttingRecipeList.Getoutput(GetKitchenObject().GetKitchenObjectDef());
            Cut();
            if (output != null) {
                DestroyKitchenObject();
                if(kitchenObjectList.TryGetKitchenGameObject(output, out GameObject gameObject)) {
                    CreateKitchenObject(gameObject);
                }
                
            }
        }
    }

    private void Cut() {
        cuttingCounterVisual.PlayCut();
    }
}
