using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput input;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 480f;

    private Vector3 _lastDirection;
    private bool isWalking;

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        var inputVector2 = input.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector2.x, 0, inputVector2.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirx = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirx, moveDistance);
            if (canMove)
            {
                moveDir = moveDirx;
            } else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }


        isWalking = moveDir.magnitude > 0;
        if (moveDir.magnitude != 0)
        {
            var quaternion = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, rotateSpeed * Time.deltaTime);
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        var movementVectorNormalized = input.GetMovementVectorNormalized();
        if (movementVectorNormalized != Vector2.zero)
        {
            _lastDirection = movementVectorNormalized;
        }

        float interactionDistance = 2f;
        RaycastHit hit;
        Physics.Raycast(transform.position, _lastDirection, out hit, interactionDistance);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        } else
        {
            Debug.Log("-");
        }
    }
}