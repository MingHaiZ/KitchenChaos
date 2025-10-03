using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput input;

    [SerializeField] private float moveSpeed = 5f;

    private bool isWalking;

    private void Update()
    {
        var inputVector2 = input.GetMovementVectorNormalized();
        var normalized = new Vector3(inputVector2.x, 0, inputVector2.y);
        isWalking = normalized.magnitude > 0;
        if (normalized.magnitude != 0)
        {
            var quaternion = Quaternion.LookRotation(normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 180f * Time.deltaTime);

            transform.position += normalized * moveSpeed * Time.deltaTime;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}