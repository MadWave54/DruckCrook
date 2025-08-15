using UnityEngine;

public class PlayerTrail : MonoBehaviour
{

    public float speed = 5f;

    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position += move;

    }

}
