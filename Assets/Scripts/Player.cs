using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameInput gameInput;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float rotateSpeed = 10f;
    [SerializeField]
    private float playerRadius = 0.6f;
    [SerializeField]
    private float playerHeight = 2f;

    private bool isWalking;

    private void FixedUpdate()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        Vector3 rayDistance = transform.position + Vector3.up * playerHeight;

        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position,
            rayDistance, playerRadius, moveDir, moveDistance);

        if(!canMove) //Attempt only X movement
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position,
                rayDistance, playerRadius, moveDirX, moveDistance);

            if (canMove) //Can move only X
            {
                moveDir = moveDirX;
            }
            else //Attempt onle Z movement
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position,
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
}
