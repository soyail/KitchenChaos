using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StoveCounter : BaseCounter
{
    private enum StoveState {
        Idle,
        Frying,
        Burning
    };
    [SerializeField] private FryingRecipeList fryingRecipeList;
    [SerializeField] private KitchenObjectList kitchenObjectList;
    
    private FryingRecipe fryingRecipe;
    private float fryingTime;
    private StoveState stoveState;
    public override void Interact(Player player) {
        if (player.IsHaveKitchenObject()&&
            fryingRecipeList.TryGetFryingRecipe(player.GetKitchenObject().GetKitchenObjectDef(), out fryingRecipe)) {
            if (!IsHaveKitchenObject()) {
                TransferKitchenObject(player, this);
                StartFrying(fryingRecipe);
            }
            else {
                Debug.Log("Nothing to do");
            }
        }
        else {
            if (IsHaveKitchenObject()) {
                TransferKitchenObject(this, player);
                stoveState = StoveState.Idle;
            }
            
        }
    }
    public void StartFrying(FryingRecipe fryingRecipe) {
        fryingTime = 0.0f;
        stoveState = StoveState.Frying;
    }
    private void Start() {
        stoveState = StoveState.Idle;
    }
    private void Update() {
        switch(stoveState) {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTime += Time.deltaTime;
                if (fryingTime > fryingRecipe.fryingTime) {
                    DestroyKitchenObject();
                    if(kitchenObjectList.TryGetKitchenGameObject(fryingRecipe.output, out GameObject gameObject)) {
                        CreateKitchenObject(gameObject);
                    }
                    stoveState = StoveState.Burning;
                    fryingTime = 0.0f;
                };
                break;
            case StoveState.Burning:
                break;
        }
        
    }

}
