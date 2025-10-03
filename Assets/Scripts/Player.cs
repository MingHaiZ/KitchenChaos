using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 5f;

    private void Update()
    {
        var inputVector2 = new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            inputVector2.y += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector2.x -= 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector2.y -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector2.x += 1;
        }

        inputVector2 = inputVector2.normalized;
        var normalized = new Vector3(inputVector2.x, 0, inputVector2.y);
        var quaternion = Quaternion.LookRotation(normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 180f*Time.deltaTime);

        transform.position += normalized * moveSpeed * Time.deltaTime;
    }
}