using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private KitchenObjectDef plateDef;
    [SerializeField] private KitchenObjectDef plateDirtyDef;
    [SerializeField] private KitchenObjectList kitchenObjectList;
    [SerializeField] private int plateMaxCount;
    [SerializeField] private float spawnRate;
    private float timer = 0;
    private Stack<KitchenObject> platesIdle = new Stack<KitchenObject>();
    private int platesToClean = 0;
    private enum CurState {
        Idle,
        Busy
    }
    private CurState curState = CurState.Idle;  
    public override void Interact(Player player) {
        if (player.GetKitchenObject() == null) {
            if (platesIdle.Count > 0) {
                TakePlate(player);
            }
        }
        else {
            if (player.GetKitchenObject().GetKitchenObjectDef() == plateDirtyDef) {
                if (curState == CurState.Idle) {
                    player.DestroyKitchenObject();
                    platesToClean++;
                    curState = CurState.Busy;
                }
            }
        }
        
        
    }
    private void TakePlate(Player player) {
        Destroy(platesIdle.Pop().gameObject);
        if (kitchenObjectList.TryGetKitchenGameObject(plateDef, out GameObject gameObject)) {
            player.CreateKitchenObject(gameObject);
        }

            
    }
    private void Start() {
        while(platesIdle.Count < plateMaxCount) {
            SpawnPlate();
        }
    }
    private void Update() {
        if(curState == CurState.Busy) {
            timer += Time.deltaTime;
            if (timer > spawnRate) {
                timer = 0;
                CleanPlate();
                SpawnPlate();
                if(platesToClean == 0) {
                    curState = CurState.Idle;   
                }
            }
        }
        
        
    }
    private void CleanPlate() {
        platesToClean--;    
    }

    private void SpawnPlate() {
        if(kitchenObjectList.TryGetKitchenGameObject(plateDef, out GameObject gameObject)) {
            KitchenObject kitchenObject = GameObject.Instantiate(gameObject, GetHoldPoint()).GetComponent<KitchenObject>();
            kitchenObject.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * platesIdle.Count;
            platesIdle.Push(kitchenObject);
        }
        
    }

    

}
