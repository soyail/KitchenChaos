using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Player : KitchenObjectHolder
{
    public static Player Instance { get; private set; }
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    private bool isWalking = false;
    private BaseCounter selectedCounter;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        gameInput.onInteractAction += GameInput_onInteractAction;
        gameInput.onOperateAction += GameInput_onOperateAction;
    }

    private void GameInput_onOperateAction(object sender, System.EventArgs e) {
        selectedCounter?.InteractOperate(this);
    }

    private void GameInput_onInteractAction(object sender, System.EventArgs e) {
        selectedCounter?.Interact(this);
    }

    private void Update() {
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        

    }
    
    public bool IsWalking {
        get
        {
            return isWalking;
        }
    }

    private void HandleMovement() {
        Vector3 moveDir = gameInput.GetMovementDirectionNormalized();
        transform.position += moveDir * Time.deltaTime * moveSpeed;
        if (moveDir != Vector3.zero) {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }

    private void HandleInteraction() {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo, 2f, counterLayerMask)) {
            if(hitinfo.collider.TryGetComponent<BaseCounter>(out BaseCounter counter)) {
                SetSelectedCounter(counter);
            }
            else {
                SetSelectedCounter(null);
            }
        }
        else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter counter) {
        if ((counter!=selectedCounter))
        {
            selectedCounter?.CancelSelect();
            counter?.SelectCounter();
        }
        this.selectedCounter = counter;
    }

    private void TransferFromCounter() {

    }
   
}
