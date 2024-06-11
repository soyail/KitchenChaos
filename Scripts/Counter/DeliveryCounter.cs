using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    [SerializeField] private RecipeList recipeList;
    public override void Interact(Player player) {
        if (player.IsHaveKitchenObject()) {
            if(player.GetKitchenObject().TryGetComponent<Plate>(out Plate plate)) {
                if (OrderManager.Instance.TryGetValidOrder(plate.curKitchenObjects, out Order order)) {
                    player.DestroyKitchenObject();
                    OrderManager.Instance.RemoveFinishedOrder(order);
                }
            }
            
        }
    }
}
