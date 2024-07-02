using System;
using UnityEngine;

public class Player : Singleton<Player>, IKitchenObjectParent
{
    public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class SelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounterChanged;
    }

    [SerializeField]
    private GameInput gameInput;
    [SerializeField]
    private LayerMask countersLayerMask;
    [SerializeField]
    private Transform kitchenObjectHoldPoint;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float rotateSpeed = 10f;
    [SerializeField]
    private float playerRadius = 0.6f;
    [SerializeField]
    private float playerHeight = 2f;
    [SerializeField]
    private float interactDistance = 2f;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Start()
    {
        gameInput.OnInteractAction +=  GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        bool hasHit = Physics.Raycast(transform.position, lastInteractDir, 
            out RaycastHit hitInfo, interactDistance, countersLayerMask);
        if (hasHit)
        {
            if (hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        Vector3 rayDistance = transform.position + Vector3.up * playerHeight;

        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position,
            rayDistance, playerRadius, moveDir, moveDistance);

        if (!canMove) //Attempt only X movement
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position,
                rayDistance, playerRadius, moveDirX, moveDistance);

            if (canMove) //Can move only X
            {
                moveDir = moveDirX;
            }
            else //Attempt only Z movement
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position,
                    rayDistance, playerRadius, moveDirZ, moveDistance);

                if (canMove) //Can move only Z
                {
                    moveDir = moveDirZ;
                }
            }
        }
        if (canMove) //Can move in any direction
        {
            transform.position += moveDistance * moveDir;
        }

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(
            transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter kitchenObjectParent)
    {
        selectedCounter = kitchenObjectParent;

        OnSelectedCounterChanged?.Invoke(this,
            new SelectedCounterChangedEventArgs
            {
                selectedCounterChanged = selectedCounter
            });
    }

    public Transform GetSpawnPoint()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject newKitchenObject)
    {
        kitchenObject = newKitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
