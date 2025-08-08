using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform player;

    [SerializeField] private float smoothSpeed;

    private bool followPlayer = false;

    void LateUpdate()
    {

        if (!followPlayer && player.position.x >= 0)
        {

            followPlayer = true;

        }

        Vector3 desiredPosition = transform.position;

        if (followPlayer)
        {

            desiredPosition = new Vector3(player.position.x, transform.position.y, -10f);

        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

    }
}
