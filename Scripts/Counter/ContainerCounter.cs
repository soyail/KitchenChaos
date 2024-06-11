using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectDef kitchenObjectDef;
    [SerializeField] private KitchenObjectList kitchenObjectList;


    [SerializeField]private ContainerCounterVisual containerCounterVisual;

    public override void Interact(Player player) {
        if (!player.IsHaveKitchenObject()) {
            if (IsHaveKitchenObject()) {
                TransferKitchenObject(this, player);
            }
            else {
                if (kitchenObjectList.TryGetKitchenGameObject(kitchenObjectDef, out GameObject gameObject)) {
                    CreateKitchenObject(gameObject);
                }
                
                containerCounterVisual.PlayOpen();
                TransferKitchenObject(this, player);
            }
        }
        
    }

}
